
$(document).ready(function () {

    //sweetalertconfirm();
   // sweetalert();
});
function sweetalert(title,message,redirectUrl='') {
    Swal.fire({
        icon: 'info',
        title: title,
        html: message,
        footer: ''
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed & redirectUrl!='') {
            window.location.href = '/' + redirectUrl;
        } else if (result.isDenied) {
          
        }
    })

}
function sweetalertconfirm() {
    Swal.fire({
        title: 'Do you want to save the changes?',
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: 'Save',
        denyButtonText: `Don't save`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            Swal.fire('Saved!', '', 'success')
        } else if (result.isDenied) {
            Swal.fire('Changes are not saved', '', 'info')
        }
    })
}