function vueContext(options) {
    if (!options.el) throw "[VueContext]::options.el is not set";

    var app = Vue.createApp(options);

    app.config.unwrapInjectedRef = true;

    //Global Mixins
    //app.mixin(dateMixin);

    //Global Components
    //app.component('custom-input-group', compCustomInputGroup);
    app.component('c-check-box', compCheckbox);
    app.component('c-section', compSection);
    //app.component('c-created-by', compCreatedBy);

    return app.mount(options.el);
}