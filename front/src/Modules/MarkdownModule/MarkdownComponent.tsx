import React, {useState} from 'react';
import {ProcessText} from "./MarkdownAPI";

const MarkdownComponent = () => {
    const [text, setText] = useState<string>();
    const [processedText, setProcessedText] = useState<string>();

    return (
        <div>
            <input onChange={(e) => UpdatePromise(e.target.value, setText, setProcessedText)} value={text}/>
            <br/>
            <br/>
            <div>{processedText}</div>
        </div>
    );
};

let timeOut: NodeJS.Timeout;
async function UpdatePromise(text: any, setText: any, setProcessedText: any): Promise<void>{
    setText(text);
    if (timeOut)
        clearTimeout(timeOut);

    timeOut = setTimeout(async () => {
        let processedResponse = await ProcessText(text);
        let processedText = processedResponse.data.text;
        setProcessedText(processedText);
    }, 300);
}

export default MarkdownComponent;