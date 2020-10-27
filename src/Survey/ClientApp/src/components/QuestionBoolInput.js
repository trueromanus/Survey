import React from 'react';
import { QuestionInput } from './QuestionInput.js';

export class QuestionBoolInput extends QuestionInput {

    constructor(props) {
        super(props);

        this.state.currentValue = props.currentValue;
        this.state.callback = props.callback;

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        const value = event.target.checked;

        this.setState({ currentValue: value });
        this.state.callback(value);
    }

    render() {
        return (<input type="checkbox" className="form-control" checked={this.state.currentValue ? `on` : ``} onChange={this.handleChange} />);;
    }


}
