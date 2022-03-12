/*
    VUE DRAG AND DROP

    NOTE: TO UPLOAD THE FILE SHOULD apiService.UploadFile(Url, JsonObject, File(s)) BE UTILITIZED 
    
    SVG ICON: BootstrapIcon (v1.3.0); file-earmark-arrow-up
 */
Vue.component("drag-and-drop", {
    template: `
        <div class="px-2 py-4">
            <div v-show="Step === 1">
                <div class="file-drop-zone mb-2 border p-3" >
                    <div class="d-flex flex-column align-items-center justify-content-center">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 16 16" style="width: 4em" class="text-primary">
                            <path d="M8.5 11.5a.5.5 0 0 1-1 0V7.707L6.354 8.854a.5.5 0 1 1-.708-.708l2-2a.5.5 0 0 1 .708 0l2 2a.5.5 0 0 1-.708.708L8.5 7.707V11.5z"/>
                            <path d="M14 14V4.5L9.5 0H4a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h8a2 2 0 0 0 2-2zM9.5 3A1.5 1.5 0 0 0 11 4.5h2V14a1 1 0 0 1-1 1H4a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h5.5v2z"/>
                        </svg>
                        <h4 class="mt-2">Drag and Drop files here</h4>
                        <div>
                            <span>Or </span>
                            <label>
                                <button type="button" class="btn btn-sm btn-secondary">Choose file</button>
                                <input type="file" multiple class="d-none" ref="fileInput" />
                            </label>
                        </div>
                    </div>
                </div>
                <div v-show="maxsize||supportedtypes" class="d-flex justify-content-between text-gray-500">
                    <div><span v-show="supportedtypes.length > 0">Supported Formats: {{supportedtypes.join(", ")}}</span></div> 
                    <div><span v-show="maxsize > 0">Maximum file size {{maxsize | fileSize}}</span></div> 
                </div>
            </div>
            <div v-show="Step === 2" class="row">
                <strong v-show="value.length > 0">Uploaded</strong>
                <div v-for="(item, index) in value" class="col-12" :class="{'mt-3': index > 0}">
                    <slot name="uploaded-file-summary" 
                        v-bind="{
                            data: item, 
                            index: index, 
                            iconClass: fileIconClass(item.FileExtension), 
                            btnRemoveFile: ()=> removeUploadedFile(index)
                        }">
                        <div class="row mx-0">
                            <div class="col-10">
                                <div class="d-flex align-items-center">
                                    <div class="h4 mb-0 mr-2 text-gray-400">
                                        <i class="fas" :class="fileIconClass(item.FileExtension)"></i>
                                    </div>
                                    <div>
                                        <strong>{{item.FileName}}</strong>
                                        <div>{{item.LastModified | moment}} • {{item.FileSize | fileSize}}</div>
                                    </div>
                                    <button type="button" class="btn btn-sm btn-danger ml-5" v-on:click="removeUploadedFile(index)">Remove</button>
                                </div>
                            </div>                            
                        </div>
                    </slot>
                </div>
                <strong v-show="uploadError.length > 0">Failed</strong>
                <div v-for="(item, index) in uploadError" class="col-12" :class="{'mt-3': index > 0}">
                    <div class="row mx-0">
                        <div class="col-10">
                            <div class="d-flex align-items-center">
                                <div class="h4 mr-2 text-gray-400">
                                    <i class="fas" :class="fileIconClass(item.FileExtension)"></i>
                                </div>
                                <div>
                                    <strong>{{item.FileName}}</strong>
                                    <div class="text-danger">{{item.Exception}}</div>
                                </div>
                            </div>
                        </div>
                        <div class="col-2 d-flex justify-content-end align-items-center">
                            <button type="button" class="btn btn-sm btn-danger mr-1" v-on:click="removeUploadedErrorFile(index)">Remove</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `,
    props: {
        value: {
            required: true,
            default: function () { return []}
        },
        maxsize: {
            // MAX SIZE IN BYTES
            type: Number,
            required: false,
            default: function () { return 1073741824 }

        },
        supportedtypes: {
            required: false,
            validator: function (val) { return (val || []).length >= 0; },
            default: function () { return ["PNG", "JPG", "JPEG", "BMP"]}
        }
    },
    data: function () {
        return {
            uploadError: [] // {FileName, Exception}
        }
    },
    methods: {
        fileIconClass: function (extension) {
            extension = (extension || "").toLowerCase();

            if (["pdf"].indexOf(extension) > -1) return "fa-file-pdf";
            else if (["png", "jpg", "jpeg", "BMP"].indexOf(extension) > -1) return "fa-file-image";
            return "fa-file";
        },
        removeUploadedFile: function (index) {
            this.value.splice(index, 1);            
        },
        removeUploadedErrorFile: function (index) {
            this.uploadError.splice(index, 1);

            var isValid = this.uploadError.length === 0;
            this.$emit('valid', isValid);
        },
        removeDefault: function (e) {
            e.preventDefault();
            e.stopPropagation();
        },     
        addToUploadError: function (object) {
            this.uploadError.push(object)
        },
        fileDrop: function (event) {
            var droppedFiles = [];
            var dataList = [];

            var maxSize = this.maxsize;
            var supportedTypes = this.supportedtypes.map(function (x) { return x.toLowerCase()});
            var addToUploadError = this.addToUploadError

            // Get the files 
            if ($(event.target).is("input")) droppedFiles = event.target.files
            else droppedFiles = event.originalEvent.dataTransfer.files;

            // Translate the FileList to the correct array object type
            deferedList = $(droppedFiles).toArray().map(function (file) {
                var deffered = $.Deferred();


                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {

                    var obj = {
                        FileName: file.name,
                        FileSize: file.size,
                        FileExtension: file.name.split('.').pop(),
                        LastModified: file.lastModifiedDate,
                        Base64Data: reader.result,
                        // We include the file in the object as the base64Data string above is just used to preview the image
                        File: file
                    };


                    if (file.size > maxSize) obj.Exception = "File is to big";
                    else if (supportedTypes.length > 0 && supportedTypes.indexOf(obj.FileExtension.toLowerCase()) === -1) 
                        obj.Exception = "File type is not supported";
                    
                    
                    if (!obj.Exception) dataList.push(obj)
                    else addToUploadError(obj);

                    deffered.resolve(dataList);
                }

                return deffered
            })

            // Wait for all the filereads to complete
            $.when.apply($, deferedList).done(this.fileUploadDone)
        },
        fileUploadDone: function (dataList) {
            var isValid = this.uploadError.length === 0;

            this.$emit('input', dataList);
            this.$emit('valid', isValid);
            $(this.$refs.fileInput).val(null);
        },
    },
    computed: {
        Step: function() {
            if (this.value.length === 0 && this.uploadError.length === 0) return 1;
            else return 2;
        }
    },
    mounted: function () {
        $(this.$el).off()
            .on('drag dragstart dragend dragover dragenter dragleave drop', this.removeDefault)
            .on('drop', this.fileDrop);

        $(this.$refs.fileInput).off()
            .on("change input", this.fileDrop)
    }
});