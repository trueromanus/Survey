import React from 'react';
import { QuestionInput } from './QuestionInput.js';

export class QuestionEnumInput extends QuestionInput {

    constructor(props) {
        super(props);

        this.state.currentValue = props.currentValue;
        console.log(`constructor`, this.state.currentValue);
        this.state.callback = props.callback;
        this.options = props.options;

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        const value = parseInt(event.target.value);

        this.setState({ currentValue: value });
        this.state.callback(value);
    }

    render() {
        return (
            <select className="form-control" value={this.state.currentValue + ``} onChange={this.handleChange}>
                {this.options.map((a) => <option value={a.id} key={a.id}>{a.title}</option>)}
            </select>
        );
	}


}
