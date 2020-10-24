import React, { Component } from 'react';
import './Survey.css';

const valueTypes = {
    String: 1,
    Integer: 2,
    Enum: 3,
    Bool: 4,
    Date: 5
};

export class QuestionForm extends Component {

    constructor(props) {
        super(props);
        this.state = {
            forecasts: [],
            questions: [],
            currentQuestionIndex: 0,
            currentValue: ``,
            answers: [],
            loading: true,
            finalMessage: ``
        };
        this.handleChange = this.handleChange.bind(this);
        this.next = this.next.bind(this);
        this.previous = this.previous.bind(this);
        this.save = this.save.bind(this);
        this.handleCheckBox = this.handleCheckBox.bind(this);
        this.handleNumber = this.handleNumber.bind(this);
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
        if (question.valueType === valueTypes.Bool && answer.value === ``) answer.value = false;
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

    handleChange(event) {
        this.setState({ currentValue: event.target.value });
    }

    handleNumber(event) {
        this.setState({ currentValue: parseInt(event.target.value) });
    }

    handleCheckBox(event) {
        this.setState({ currentValue: event.target.checked });
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
        
        let inputContent;
        const question = questions[currentQuestion];
        if (question) {
            switch (question.valueType) {
                case valueTypes.String:
                    inputContent = (<input type="text" className="form-control" value={this.state.currentValue} onChange={this.handleChange} />);
                    break;
                case valueTypes.Integer:
                    inputContent = (<input type="number" min="0" max="130" step="1" className="form-control" value={this.state.currentValue + ``} onChange={this.handleNumber} />);
                    break;
                case valueTypes.Bool:
                    inputContent = (<input type="checkbox" className="form-control" checked={this.state.currentValue ? `on` : ``} onChange={this.handleCheckBox} />);
                    break;
                case valueTypes.Enum:
                    const options = [{id: null, title: ``}].concat(question.questionOptions);
                    inputContent = (
                        <select className="form-control" value={this.state.currentValue + ``} onChange={this.handleNumber}>
                            {options.map((a) => <option value={a.id} key={a.id}>{a.title}</option>)}
                        </select>
                    );
                    break;
                case valueTypes.Date:
                    inputContent = (<input type="date" className="form-control" value={this.state.currentValue} onChange={this.handleChange} />);
                    break;
                default:
                    console.warn(`Not supported value type!`);
                    break;
            }
        }

        return (
            <div>
                <span>{question ? question.title : `Вы все заполнили, сохраните результат`}</span>
                {inputContent}
                <div className="navigation-buttons">
                    <button type="button" className="btn btn-primary" onClick={this.previous} disabled={!(currentQuestion > 0)}>Назад</button>
                    <button type="button" className="btn btn-primary" onClick={this.next} disabled={!(currentQuestion < questions.length) || (!this.state.currentValue && question.valueType !== valueTypes.Bool)}>Вперед</button>
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
