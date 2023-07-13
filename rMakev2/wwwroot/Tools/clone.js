// Create a new function to be added to the menu

class CloneTool {

    static get isTune() {
        return true;
    }

    constructor({ api, config, block, data }) {
        this.api = api;
        this.dotNetRef = config.dotNet
        this.block = block
        this.data = data

        this.wrapper = undefined;
    }

    render() {

        const cloneWrapper = {
            title: 'Clone',
            icon: '<svg width="17" height="15" viewBox="0 0 336 276" xmlns="http://www.w3.org/2000/svg"><path d="M291 150V79c0-19-15-34-34-34H79c-19 0-34 15-34 34v42l67-44 81 72 56-29 42 30zm0 52l-43-30-56 30-81-67-66 39v23c0 19 15 34 34 34h178c17 0 31-13 34-29zM79 0h178c44 0 79 35 79 79v118c0 44-35 79-79 79H79c-44 0-79-35-79-79V79C0 35 35 0 79 0z"/></svg>',
            onActivate: () => {
                this.onClick()
            }
        };

        return cloneWrapper;
    }

    onClick() {

        console.log(this.api.blocks.getBlockByIndex(this.api.blocks.getCurrentBlockIndex() - 1))
        console.log(this.dotNetRef)

        this.dotNetRef.invokeMethodAsync('LoadPortfolio')

    }




}