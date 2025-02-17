$(document).ready(function () {
    // Enable/disable military service dropdown based on gender selection
    $('#ddlGender').change(function () {
        if ($(this).val() == 1) { // 1 = Male
            $('#ddlMilitaryService').prop('disabled', false);
        } else {
            $('#ddlMilitaryService').prop('disabled', true);
        }
    });
});
