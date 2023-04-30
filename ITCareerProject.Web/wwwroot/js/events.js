$('.real-submit').hide();

let imageIsValid = true;

$(document).on("change", ".file-input", function () {
    console.log('file selected');
    var fileSize = this.files[0].size; // in bytes
    if (fileSize > 2 * 1024 * 1024) { // 2MB in bytes
        imageIsValid = false;
        Swal.fire(
            'Error!',
            "File size exceeds 2MB. Please select a smaller file.",
            'error'
        );

        $(this).val(""); // clear the file input field
    } else {
        imageIsValid = true;
    }
});

$(document).on("input",
    ".name-input",
    function (e) {
        let inputValue = $(this).val();
        if (inputValue.length > 64) {
            Swal.fire(
                'Error!',
                "Name should not be more than 64 characters!",
                'error'
            );

            $(this).val(inputValue.substring(0, 64));
            e.preventDefault();
        }
    });

$(document).on("input",
    ".description-input",
    function (e) {
        let inputValue = $(this).val();

        if (inputValue.length > 255) {
            Swal.fire(
                'Error!',
                "Description should not be more than 255 characters!",
                'error'
            );


            $(this).val(inputValue.substring(0, 255));

            e.preventDefault();
        }
    });

$(document).on("click",
    ".fake-submit",
    function () {
        if (imageIsValid) {
            $('.real-submit').click();
        } else {
            Swal.fire(
                'Error!',
                "File size exceeds 2MB. Please select a smaller file.",
                'error'
            );
        }
    });
