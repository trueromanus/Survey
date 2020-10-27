import React, { Component } from 'react';
import { QuestionInputFactory } from './QuestionInputFactory.js';
import './Survey.css';

export class QuestionForm extends Component {

    constructor(props) {
        super(props);
        this.state = {
            questions: [],
            currentQuestionIndex: 0,
            currentValue: ``,
            answers: [],
            loading: true,
            finalMessage: ``
        };
        this.factory = new QuestionInputFactory();
        this.next = this.next.bind(this);
        this.previous = this.previous.bind(this);
        this.save = this.save.bind(this);
    }

    componentDidMount() {
        this.loadQuestions();
    }

    next() {
        const question = this.state.questions[this.state.currentQuestionIndex];
        const answers = this.state.answers;
        let answer = answers[this.state.currentQuestionIndex];
        if (!answer) {
            answer = {};
            answers.push(answer);
        }

        answer.valueType = question.valueType;
        answer.value = this.state.currentValue;
        if (question.valueType === 4 && answer.value === ``) answer.value = false;
        answer.questionId = question.id;

        const index = this.state.currentQuestionIndex + 1;
        const oldValue = answers[index];
        this.setState({ currentQuestionIndex: index, answers: answers, currentValue: oldValue ? oldValue.value : `` });
    }

    previous() {
        const index = this.state.currentQuestionIndex - 1;
        const answer = this.state.answers[index];
        this.setState({ currentQuestionIndex: index, currentValue: answer ? answer.value : `` });
    }

    async save() {
        let message = `Приносим свои извинения. В процессе работы произошла непредвиденная ошибка!`;
        try {
            let response = await fetch('/api/answers/save', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json;charset=utf-8'
                },
                body: JSON.stringify(this.state.answers)
            });
            const result = await response.json();
            if (result) message = `Спасибо, Ваши данные успешно получены!`;
        } catch (e) {
            //Something went wrong
        }

        this.setState({ finalMessage: message });
    }

    renderQuestions(questions, currentQuestion) {
        const question = questions[currentQuestion];

        let inputContent;
        if (question) inputContent = this.factory.getQuestionInput(question.valueType, this.state.currentValue, question.questionOptions, (value) => this.setState({ currentValue: value }));

        return (
            <div>
                <span>{question ? question.title : `Вы все заполнили, сохраните результат`}</span>
                {inputContent}
                <div className="navigation-buttons">
                    <button type="button" className="btn btn-primary" onClick={this.previous} disabled={!(currentQuestion > 0)}>Назад</button>
                    <button type="button" className="btn btn-primary" onClick={this.next} disabled={!(currentQuestion < questions.length) || (!this.state.currentValue && question.valueType !== 4)}>Вперед</button>
                    <button type="button" className="btn btn-primary" onClick={this.save} disabled={!(currentQuestion === questions.length)}>Сохранить результаты</button>
                </div>
            </div>
        );
    }

    render() {
        let contents;
        if (!this.state.finalMessage) {
            contents = this.state.loading ? <p><em>Загрузка...</em></p> : this.renderQuestions(this.state.questions, this.state.currentQuestionIndex);
        } else {
            contents = <p><em>{this.state.finalMessage}</em></p>;
		}

        return (
            <div>
                <h1 id="tabelLabel" >Анкета</h1>
                <p>Заполните анкету и нажмите на кнопку Сохранить изменения.</p>
                {contents}
            </div>
        );
    }

    async loadQuestions() {
        const response = await fetch('/api/questions/list');
        const data = await response.json();
        this.setState({ questions: data, loading: false });
    }
}
