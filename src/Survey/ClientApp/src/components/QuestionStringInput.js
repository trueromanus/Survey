import React from 'react';
import { QuestionInput } from './QuestionInput.js';

export class QuestionStringInput extends QuestionInput {

    constructor(props) {
        super(props);

        this.state.currentValue = props.currentValue;
        this.state.callback = props.callback;

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        const value = event.target.value;

        this.setState({ currentValue: value });
        this.state.callback(value);
    }

    render() {
        return (<input type="text" className="form-control" value={this.state.currentValue} onChange={this.handleChange} />);
	}


}
