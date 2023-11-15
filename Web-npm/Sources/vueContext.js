function vueContext(options) {
    if (!options.el) throw "[VueContext]::options.el is not set";

    var app = Vue.createApp(options);

    app.config.unwrapInjectedRef = true;
    app.config.compilerOptions.isCustomElement = (tag) => tag.includes('--')

    //Global Mixins
    //app.mixin(dateMixin);

    //Global Components
    app.component('c-checkbox', compCheckbox);
    app.component('c-section', compSection);
    app.component('c-input', compInput);
    app.component('c-select', compSelect);
    app.component('c-button', compButton)
    app.component('c-modal', compModal)
    app.component('c-datepicker', compDatepicker)
    app.component('c-table', compTable)
    app.component('c-dropdown', compDropdown)
    app.component('c-th', compTH)
    app.component('c-td', compTD)
    app.component('c-tr', compTR)

    return app.mount(options.el);
}