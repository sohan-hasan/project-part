$(function () {
    $('.change_image').change(function () {
        var input = this;
        if (input.files) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('.change_edit').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    })
});