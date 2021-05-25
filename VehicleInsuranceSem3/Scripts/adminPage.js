function getDate(datePickerValue) {
    let initDate = new Date(datePickerValue);
    let day = ("0" + initDate.getDate()).slice(-2);
    let month = ("0" + (initDate.getMonth() + 1)).slice(-2);
    let result = initDate.getFullYear() + "-" + (month) + "-" + (day);
    return result;
}