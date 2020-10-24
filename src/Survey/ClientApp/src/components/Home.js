import React, { Component } from 'react';
import './Home.css';

export class Home extends Component {

    constructor(props) {
        super(props);

        this.startSurvey = this.startSurvey.bind(this);
    }

    startSurvey() {
        this.props.history.push('/questionform');
    }

    render () {
        return (
            <div className="full-container">
                <span>Ответьте на ряд вопрос</span>
                <div className="start-container">
                    <button type="button" className="btn btn-primary" onClick={this.startSurvey}>Начать</button>
                </div>
            </div>
        );
    }
}
