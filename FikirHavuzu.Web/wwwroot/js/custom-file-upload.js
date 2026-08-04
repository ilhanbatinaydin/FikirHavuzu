document.addEventListener("DOMContentLoaded", function () {
    const fileInput = document.getElementById('documentInput');
    const fileList = document.getElementById('fileList');

    if (!fileInput || !fileList) return;

    let dataTransfer = new DataTransfer();

    fileInput.addEventListener('change', function () {
        for (let i = 0; i < this.files.length; i++) {
            dataTransfer.items.add(this.files[i]);
        }

        this.files = dataTransfer.files;
        updateFileListUI();
    });

    function getFileIconClass(fileName) {
        const extension = fileName.split('.').pop().toLowerCase();

        switch (extension) {
            case "pdf":
                return "fa-file-pdf text-danger";
            case "doc":
            case "docx":
                return "fa-file-word text-primary";
            case "xls":
            case "xlsx":
                return "fa-file-excel text-success";
            case "jpg":
            case "jpeg":
            case "png":
                return "fa-file-image text-info";
            case "zip":
            case "rar":
                return "fa-file-zipper text-warning";
            case "ppt":
            case "pptx":
                return "fa-file-powerpoint text-warning";
            default:
                return "fa-file text-secondary";
        }
    }

    function updateFileListUI() {
        fileList.innerHTML = '';

        for (let i = 0; i < dataTransfer.files.length; i++) {
            const file = dataTransfer.files[i];

            const iconClass = getFileIconClass(file.name);

            let fileSize = (file.size / 1024 / 1024).toFixed(2) + " MB";
            if (file.size < 1024 * 1024) {
                fileSize = (file.size / 1024).toFixed(0) + " KB";
            }

            const fileItem = document.createElement('div');
            fileItem.className = 'd-flex justify-content-between align-items-center p-2 bg-light border rounded-3 shadow-sm';
            fileItem.innerHTML = `
                <div class="d-flex align-items-center text-truncate pe-3">
                    <i class="fa-solid ${iconClass} fs-3 me-3"></i>
                    <div class="d-flex flex-column text-truncate">
                        <span class="fw-bold small text-truncate" title="${file.name}">${file.name}</span>
                        <span class="text-muted" style="font-size: 0.75rem;">${fileSize}</span>
                    </div>
                </div>
                <button type="button" class="btn btn-sm btn-outline-danger rounded-circle px-2 py-1 remove-btn" data-index="${i}" title="Dosyayı Çıkar">
                    <i class="fa-solid fa-xmark"></i>
                </button>
            `;
            fileList.appendChild(fileItem);
        }

        document.querySelectorAll('.remove-btn').forEach(button => {
            button.addEventListener('click', function () {
                const indexToRemove = parseInt(this.getAttribute('data-index'));

                const newDataTransfer = new DataTransfer();
                for (let i = 0; i < dataTransfer.files.length; i++) {
                    if (i !== indexToRemove) {
                        newDataTransfer.items.add(dataTransfer.files[i]);
                    }
                }

                dataTransfer = newDataTransfer;
                fileInput.files = dataTransfer.files;

                updateFileListUI();
            });
        });
    }
});