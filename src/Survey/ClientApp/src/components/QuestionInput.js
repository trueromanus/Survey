import { Component } from 'react';

export class QuestionInput extends Component {

    constructor(props) {
        super(props);
        this.state = {
            currentValue: props.currenValue,
            callback: props.callback
        };
    }

}
