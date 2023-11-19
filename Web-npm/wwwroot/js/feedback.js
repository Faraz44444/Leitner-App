var feedbackHandler = (() => {

    const toastContainer = document.getElementById("toastContainer");

    function showError(title = '', message = '') {
        if (!title || title.trim() == '')
            title = 'Oops!'
        if (!message || message.trim() == '')
            message = 'Noe gikk galt!'

        let modal= document.getElementById('errorModal');
        let modalTitle = document.getElementById('errorModalTitle');
        let modalMessage = document.getElementById('errorModalMessage');
        modalTitle.innerHTML = title;
        modalMessage.innerHTML = message;
        modal.style.display = '';
    }

    function showMessage(title = '', message = '') {
        if (!title || title.trim() == '')
            title = 'Obs!'
        if (!message || message.trim() == '')
            return;

        let modalTitle = document.getElementById('messageModalTitle');
        let modalMessage = document.getElementById('messageModalMessage');
        modalTitle.innerHTML = title;
        modalMessage.innerHTML = message;

        messageModal.toggle();
    }

    const toastTemplate = `<div class="d-flex">
                                <div class="toast-body">
                                    <h5>{content}</h5>
                                </div>
                                <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
                            </div>`;

    function showToast(message = '') {
        let id = 'id' + (new Date()).getTime();

        let toast = document.createElement('div');
        toast.setAttribute("class", "toast align-items-center bg-white border-danger border-4 rounded-0 border-top-0 border-end-0 border-bottom-0");
        toast.setAttribute("role", "alert");
        toast.setAttribute("aria-live", "assertive");
        toast.setAttribute("aria-atomic", "true");
        toast.setAttribute("data-bs-delay", "2500");
        toast.setAttribute("id", id);

        let toastContent = toastTemplate.replace("{content}", message);

        toast.innerHTML = toastContent;
        toastContainer.appendChild(toast);

        var toastEl = new bootstrap.Toast(toast, {});
        toastEl.show();
    }

    return {
        ShowError: showError,
        ShowMessage: showMessage,
        ShowToast: showToast
    }
})();