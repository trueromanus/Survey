import React from 'react';
import { QuestionStringInput } from './QuestionStringInput.js';
import { QuestionEnumInput } from './QuestionEnumInput.js';
import { QuestionNumberInput } from './QuestionNumberInput.js';
import { QuestionBoolInput } from './QuestionBoolInput.js';
import { QuestionDateInput } from './QuestionDateInput.js';

const valueTypes = {
    string: 1,
    integer: 2,
    enum: 3,
    bool: 4,
    date: 5
};

export class QuestionInputFactory {

    getQuestionInput(valueType, value, additionalValue, callback) {
        switch (valueType) {
            case valueTypes.string:
                return <QuestionStringInput currentValue={value} callback={callback} />;
            case valueTypes.integer:
                return <QuestionNumberInput currentValue={value} callback={callback} />;
            case valueTypes.bool:
                return <QuestionBoolInput currentValue={value} callback={callback} />;
            case valueTypes.enum:
                const options = [{ id: null, title: `` }].concat(additionalValue);
                return <QuestionEnumInput currentValue={value} options={options} callback={callback} />;
            case valueTypes.date:
                return <QuestionDateInput currentValue={value} callback={callback} />;
            default:
                console.warn(`Not supported value type!`);
                break;
        }
    }

}
