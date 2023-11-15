const template = document.createElement('template');
template.innerHTML = `
    <style>
        h3 {
            color: coral;
        }   
    </style>
    <div class="info">
        <h4><slot name="name"/></h4>
    </div>
    <button type="button" id="toggle-info">Hide Info</button>
`
class MyUsername extends HTMLElement {
    constructor() {
        // Constructor of HTMLElement, the extending class
        super();

        this.showInfo = true;


        // makes a shodow dom which means whatever is inside the component is in a separate dom
        // the outside will not effect our component and our component won't effect the outside
        this.attachShadow({ mode: 'open' });
        // appending the defined element to the created shadow dom
        this.shadowRoot.appendChild(template.content.cloneNode(true));
        //this.shadowRoot.querySelector('h3').innerText = this.getAttribute('name');
    }
    toggleInfo() {
        this.showInfo = !this.showInfo;

        const info = this.shadowRoot.querySelector(".info");
        const toggleBtn = this.shadowRoot.querySelector("#toggle-info");

        if (this.showInfo) {
            info.style.display = 'block';
            toggleBtn.innerHTML = 'Hide Info';
        }
        else {
            info.style.display = 'none';
            toggleBtn.innerHTML = 'Show Info';
        }
    }
    // Called every time the element is inserted into the DOM
    connectedCallback() {
        this.shadowRoot.querySelector('#toggle-info').addEventListener('click', () => this.toggleInfo());
    }
    // Called every time the element is removed from the DOM
    disconnectedCallback() {
        this.shadowRoot.querySelector('#toggle-info').removeEventListener();
    }
}

window.customElements.define('my--username', MyUsername)