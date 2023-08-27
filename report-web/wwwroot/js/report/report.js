

$(document).ready(function () {

    loadProjectList();
    loadMasterProjectList();
   
    $(".hamburger .hamburger__inner").click(function () {
        $(".wrapper").toggleClass("active")
    })

    $(".right_menu").click(function () {
        $(".profile_dd").toggleClass("active deactive");
    });
    $('#project-popup').css({
        'display': 'none'
    });
    $('#UpdateHeader').css({
        'display': 'none'
    });
    $('#AddHeader').css({
        'display': 'block'
    });
    $("#SubmitReportButton").click(function () {

        if (!ValidateReportForm(CKEDITOR.instances))
            return false;

        let project = $("select[name='Project']").val().trim().toLowerCase();
        $("#ProjectView").text($('#Project option:selected').text());
        $("#IsActiveView").text($("input[name='IsActive']:checked").val());
        $("#ProjectNameView").text($("input[name='ProjectName']").val());
        $("#SpocView").text($("input[name='Spoc']").val());
        $("#IblPriorityView").text($("input[name='IblPriority']").val());
        $("#CurrentPhaseView").text($("input[name='CurrentPhase']").val());
        $("#StatusView").text($("input[name='Status']").val());
        $("#NextPhaseView").text($("input[name='NextPhase']").val());
        $("#CrDetailsView").html(CKEDITOR.instances.CrDetailsArea.getData());
        $("#RagStatusView").text($('#RagStatus option:selected').val()); 
        $("#CurrentProgressView").html(CKEDITOR.instances.CurrentProgressArea.getData());
        $("#DevStartDateView").text(project.includes('dev') ? $("input[name='DevStartDate']").val() : '');
        $("#UatReleaseDateView").text(project.includes('uat') ? $("input[name='UatReleaseDate']").val() : '');
        $("#UatSignoffDateView").text(project.includes('uat') ? $("input[name='UatSignoffDate']").val() : '');
        $("#PreProdReleaseDateView").text(project.includes('pre') ? $("input[name='PreProdReleaseDate']").val() : '');
        $("#PreProdSignoffDateView").text(project.includes('pre') ? $("input[name='PreProdSignoffDate']").val() : '');
        $("#ProdReleaseDateView").text(project.includes('prod') ? $("input[name='ProdReleaseDate']").val() : '');

        $('#add-project').css({
            'display': 'none'
        });
        $('#project-popup').css({
            'display': 'block'
        });

    });
    $("#SubmitViewBtn").click(function () {
        $('#SubmitViewBtn').attr('disabled', 'disabled');
        $('#NoBtn').attr('disabled', 'disabled');
        var token = $("input[name='__RequestVerificationToken']").val();
        let project = $("select[name='Project']").val().trim().toLowerCase();
        const report = {
            "Id": $("#ProjectId").val(),
            "UserId": parseInt($("input[name='UserId']").val()),
            "ProjectMasterId": $('#Project option:selected').attr("hidden-attr"),
            "IsActive": $("input[name='IsActive']:checked").val().toLowerCase() == 'yes' ? 1 : 0,
            "ProjectName": $("input[name='ProjectName']").val(),
            "Spoc": $("input[name='Spoc']").val(),
            "IblPriority": parseInt($("input[name='IblPriority']").val()),
            "CurrentPhase": $("input[name='CurrentPhase']").val(),
            "Status": $("input[name='Status']").val(),
            "NextPhase": $("input[name='NextPhase']").val(),
            "CrDetails": CKEDITOR.instances.CrDetailsArea.getData(),
            "RagStatus": $('#RagStatus option:selected').val(),
            "CurrentProgress": CKEDITOR.instances.CurrentProgressArea.getData(),
            "DevStartDate": project.includes('dev') ? $("input[name='DevStartDate']").val() : '',
            "UatReleaseDate": project.includes('uat') ? $("input[name='UatReleaseDate']").val() : '',
            "UatSignoffDate": project.includes('uat') ? $("input[name='UatSignoffDate']").val() : '',
            "PreProdReleaseDate": project.includes('pre') ? $("input[name='PreProdReleaseDate']").val() : '',
            "PreProdSignoffDate": project.includes('pre') ? $("input[name='PreProdSignoffDate']").val() : '',
            "ProdReleaseDate": project.includes('prod') ? $("input[name='ProdReleaseDate']").val() : '',
            RequestVerificationToken: token
        };

        fetch("/Dashboard/AddNewReport", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token
            },
            body: JSON.stringify(report)
        })
            .then(
                response => response.json()
            )
            .then(data => {
                $('#SubmitViewBtn').removeAttr('disabled');
                $('#NoBtn').removeAttr('disabled');
                if (data.response.returnCode == 200) {
                    clearForm();
                    sweetalert("Message", data.response.returnMsg);
                    $('#project-popup').css({
                        'display': 'none'
                    });
                    $('#add-project').css({
                        'display': 'block'
                    });
                    loadProjectList();
                    ClickBtnById("dash-menu-click");
                }
                else
                    sweetalert("Error", "Something went wrong !");
            })
            .catch(error => {
                $('#SubmitViewBtn').removeAttr('disabled');
                $('#NoBtn').removeAttr('disabled');
                console.error("Error:", error);
                sweetalert("Error", "Something went wrong !")
            });

    });
    $("#NoBtn").click(function () {
        $('#add-project').css({
            'display': 'block'
        });
        $('#project-popup').css({
            'display': 'none'
        });
    });
    $('#ProjectDropDown').on('change', function () {

        loadProjectList();
        
    });
    $('#Project').on('change', function () {
        var selectedOption = $(this).val().trim().toLowerCase();

        // Enable or disable specific field based on selected option

        if (selectedOption.includes('dev')) {
            $('#DevStartDateSection').css({
                'display': 'flex'
            });
        }
        else {
            $('#DevStartDateSection').css({
                'display': 'none'
            });
        }
        if (selectedOption.includes('uat')) {
            $('#UatReleaseDateSection').css({
                'display': 'flex'
            });
            $('#UatSignoffDateSection').css({
                'display': 'flex'
            });
        }
        else {
            $('#UatReleaseDateSection').css({
                'display': 'none'
            });
            $('#UatSignoffDateSection').css({
                'display': 'none'
            });
        }
        if (selectedOption.includes('pre')) {
            $('#PreProdReleaseDateSection').css({
                'display': 'flex'
            });
            $('#PreProdSignoffDateSection').css({
                'display': 'flex'
            });
        }
        else {
            $('#PreProdReleaseDateSection').css({
                'display': 'none'
            });
            $('#PreProdSignoffDateSection').css({
                'display': 'none'
            });
        }
        if (selectedOption.includes('prod')) {
            $('#ProdReleaseDateSection').css({
                'display': 'flex'
            });
        }
        else {
            $('#ProdReleaseDateSection').css({
                'display': 'none'
            });
        }
        mandatoryProjectSelection(false);

    });
    $("#ResetBtn").click(function () {
        clearForm();
    });
    $("#ExportToExcel").click(function () {
        let id = $('#ProjectDropDown option:selected').attr("hidden-attr");
        fetch(`/Dashboard/ProjectExport?id=${id}`,
            {
                method: "GET"
            })
            .then(
                response => response.blob()
            )
            .then(data => {
                const blobUrl = URL.createObjectURL(data);

                // Create a temporary anchor element
                const $tempAnchor = $("<a>")
                    .attr("href", blobUrl)
                    .attr("download", "ProjectExport.xls"); // Specify the desired filename

                // Append the anchor element to the body and trigger a click event
                $("body").append($tempAnchor);
                $tempAnchor[0].click();

                // Clean up: Revoke the Blob URL and remove the anchor element
                $tempAnchor.remove();
                URL.revokeObjectURL(blobUrl);

            })
            .catch(error => {
                sweetalert("Error", "Unable to download excel!")
            });
    });

    mandatoryProjectSelection(true);
    function mandatoryProjectSelection(value) {
        if (value == true) {
            $('#ProjectName').prop('disabled', true);
            $('#Spoc').prop('disabled', true);
            $('#IblPriority').prop('disabled', true);
            $('#CurrentPhase').prop('disabled', true);
            $('#Status').prop('disabled', true);
            $('#NextPhase').prop('disabled', true);
            $('#RagStatus').prop('disabled', true);
            $('#DevStartDateSection').css({
                'display': 'none'
            });
            $('#UatReleaseDateSection').css({
                'display': 'none'
            });
            $('#UatSignoffDateSection').css({
                'display': 'none'
            });
            $('#PreProdReleaseDateSection').css({
                'display': 'none'
            });
            $('#PreProdSignoffDateSection').css({
                'display': 'none'
            });
            $('#ProdReleaseDateSection').css({
                'display': 'none'
            });
        }
        if (value == false) {
            $('#ProjectName').prop('disabled', false);
            $('#Spoc').prop('disabled', false);
            $('#IblPriority').prop('disabled', false);
            $('#CurrentPhase').prop('disabled', false);
            $('#Status').prop('disabled', false);
            $('#NextPhase').prop('disabled', false);
            $('#RagStatus').prop('disabled', false);
        }
    }
    function loadProjectList() {
      let id =  $('#ProjectDropDown option:selected').attr("hidden-attr");
        fetch(`/Dashboard/GetProjectList?id=${id}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(
                response => response.json()
            )
            .then(data => {
                if (data.response.returnCode == 200) {
                    $('#ProjectList').empty();
                    $.each(data.response.data, function (index, item) {
                        var milestoneHtml = '';

                        if (item.devStartDate.trim() === "") { }
                        else milestoneHtml += '<p><span class="bold-text">Dev Start Date:</span>&nbsp' + item.devStartDate + '</p><br>'; 

                        if (item.uatReleaseDate.trim() === "") { }
                        else milestoneHtml += '<p><span class="bold-text">Uat Release Date:&nbsp</span>' + item.uatReleaseDate + '</p><br>';

                        if (item.uatSignoffDate.trim() === "") { }
                        else milestoneHtml += '<p><span class="bold-text">Uat Signoff Date:&nbsp</span>' + item.uatSignoffDate + '</p><br>';

                        if (item.preProdReleaseDate.trim() === "") { }
                        else milestoneHtml += '<p><span class="bold-text">PreProd Release Date:&nbsp</span>' + item.preProdReleaseDate + '</p><br>';

                        if (item.preProdSignoffDate.trim() === "") { }
                        else milestoneHtml += '<p><span class="bold-text">PreProd Signoff Date:&nbsp</span>' + item.preProdSignoffDate + '</p><br>';

                        if (item.prodReleaseDate.trim() === "") { }
                        else milestoneHtml += '<p><span class="bold-text">Prod Release Date:&nbsp</span>' + item.prodReleaseDate + '</p><br>';

                        var itemHtml = ' <tr class="black-border">' +
                                        '<td>'+item.id+' </td>'+
                                        '<td>' + item.projectHead+' </td>'+
                                        '<td>' + item.projectName+' </td>'+
                                        '<td>' + item.spoc+' </td>'+
                                        '<td>' + item.iblPriority+' </td>'+
                            '<td class="miles">' + milestoneHtml +' </td>'+
                            '<td>' + item.currentPhase+' </td>'+
                            '<td>' + item.status+' </td>'+
                            '<td>' + item.nextPhase+' </td>'+
                            '<td>' + item.crDetails+' </td>'+
                            '<td>' + item.ragStatus+' </td>'+
                            '<td class="content">' + item.currentProgress+' </td>'+
                            '<td>' + item.isActive + ' </td>' +
                            '<td> <button class="editbtn" onclick="return EditProject(' + item.id + ')" type="button" >Edit </button></td>' +
                                        '</tr > '
                        $('#ProjectList').append(itemHtml);
                        
                    });
                }
                else
                    sweetalert("Error", data.response.returnMsg);
            })
            .catch(error => {
                console.error("Error:", error);
                sweetalert("Error", "Something went wrong !")
            });
    }
    function loadMasterProjectList() {
        fetch("/Dashboard/GetMasterProjectList", {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(
                response => response.json()
            )
            .then(data => {
                if (data.response.returnCode == 200) {
                    $('#Project').empty();
                    $('#ProjectDropDown').empty();
                    var projectHtml = "<option disabled selected value=''>Select Project</option>";
                    var projectDropDownHtml = "<option selected>All Projects</option>";
                    $.each(data.response.data, function (index, item) {
                        projectHtml += '<option value="' + item.environment + '" hidden-attr="' + item.id + '" >' + item.project + '</option>';
                        projectDropDownHtml += '<option value="' + item.environment + '" hidden-attr="' + item.id + '" >' + item.project + '</option>';
                    });
                    $('#Project').append(projectHtml);
                    $('#ProjectDropDown').append(projectDropDownHtml);
                }
                else
                    sweetalert("Error", data.response.returnMsg);
            })
            .catch(error => {
                console.error("Error:", error);
                sweetalert("Error", "Something went wrong !")
            });
    }
   

})
function ValidateReportForm(instances) {
  
      var project = document.getElementById("Project").value;
      var projectName = document.getElementById("ProjectName").value;
      var spoc = document.getElementById("Spoc").value;
      var iblPriority = document.getElementById("IblPriority").value;

      var devStartDate = document.getElementById("DevStartDate").value;
      var uatReleaseDate = document.getElementById("UatReleaseDate").value;
      var uatSignoffDate = document.getElementById("UatSignoffDate").value;
      var preProdReleaseDate = document.getElementById("PreProdReleaseDate").value;
      var preProdSignoffDate = document.getElementById("PreProdSignoffDate").value;
      var prodReleaseDate = document.getElementById("ProdReleaseDate").value;

      var currentPhase = document.getElementById("CurrentPhase").value;
      var status = document.getElementById("Status").value;
      var nextPhase = document.getElementById("NextPhase").value;

      var ragStatus = document.getElementById("RagStatus").value;

  // Regular expression for email validation
  //var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

      var isValid = true;

      // Clear previous error messages
      document.getElementById("ProjectNameError").innerHTML = "";
      document.getElementById("SpocError").innerHTML = "";
      document.getElementById("IblPriorityError").innerHTML = "";

    document.getElementById("DevStartDateError").innerHTML = "";
    document.getElementById("UatReleaseDateError").innerHTML = "";
    document.getElementById("UatSignoffDateError").innerHTML = "";
    document.getElementById("PreProdReleaseDateError").innerHTML = "";
    document.getElementById("PreProdSignoffDateError").innerHTML = "";
    document.getElementById("ProdReleaseDateError").innerHTML = "";

    document.getElementById("CurrentPhaseError").innerHTML = "";
    document.getElementById("StatusError").innerHTML = "";
    document.getElementById("NextPhaseError").innerHTML = "";
    document.getElementById("CrDetailsError").innerHTML = "";
    document.getElementById("RagStatusError").innerHTML = "";
    document.getElementById("CurrentProgressError").innerHTML = "";

  // Perform validation
    if (project.trim() === "") {
        document.getElementById("ProjectNameError").innerHTML = "Please select project.";
        isValid = false;
    }
    else
    {
        let projectValue = project.trim().toLowerCase();
        if (projectValue.includes('dev')) {
            if (devStartDate.trim() === "") {
                document.getElementById("DevStartDateError").innerHTML = "Please enter dev start Date.";
                isValid = false;
            }
        }
        if (projectValue.includes('uat')) {
            if (uatReleaseDate.trim() === "") {
                document.getElementById("UatReleaseDateError").innerHTML = "Please enter uat release date.";
                isValid = false;
            }
            if (uatSignoffDate.trim() === "") {
                document.getElementById("UatSignoffDateError").innerHTML = "Please enter uat sign off date.";
                isValid = false;
            }
        }
        if (projectValue.includes('pre')) {
            if (preProdReleaseDate.trim() === "") {
                document.getElementById("PreProdReleaseDateError").innerHTML = "Please enter pre prod release date.";
                isValid = false;
            }
            if (preProdSignoffDate.trim() === "") {
                document.getElementById("PreProdSignoffDateError").innerHTML = "Please enter pre prod sign off date.";
                isValid = false;
            }

        }
        if (projectValue.includes('prod')) {
            if (prodReleaseDate.trim() === "") {
                document.getElementById("ProdReleaseDateError").innerHTML = "Please enter pre prod release date.";
                isValid = false;
            }
        }

    }
    if (projectName.trim() === "") {
        document.getElementById("ProjectNameError").innerHTML = "Please enter project name.";
        isValid = false;
    }
    if (spoc.trim() === "") {
        document.getElementById("SpocError").innerHTML = "Please enter spoc.";
        isValid = false;
    }
    if (isNaN(iblPriority) || iblPriority <= 0) {
        document.getElementById("IblPriorityError").innerHTML = "Please enter ibl priority.";
        isValid = false;
    }

    if (currentPhase.trim() === "") {
        document.getElementById("CurrentPhaseError").innerHTML = "Please current phase.";
        isValid = false;
    }
    if (status.trim() === "") {
        document.getElementById("StatusError").innerHTML = "Please enter status.";
        isValid = false;
    }
    if (nextPhase.trim() === "") {
        document.getElementById("NextPhaseError").innerHTML = "Please enter next phase.";
        isValid = false;
    }
    if (instances.CrDetailsArea.getData() && instances.CrDetailsArea.getData() != null && instances.CrDetailsArea.getData().length > 0) { }
    else {
        document.getElementById("CrDetailsError").innerHTML = "Please enter cr details.";
        isValid = false;
    }
    if (ragStatus.trim() === "") {
        document.getElementById("RagStatusError").innerHTML = "Please enter rag status.";
        isValid = false;
    }
    if (instances.CurrentProgressArea.getData() && instances.CurrentProgressArea.getData() != null && instances.CurrentProgressArea.getData().length > 0) { }
    else {
        document.getElementById("CurrentProgressError").innerHTML = "Please enter current progress.";
        isValid = false;
    }

  return isValid;
}

const divsForHideShow = ["dash-menu", "new-project-menu", "ui-element-menu", "chart-menu","table-menu"];
const divsForHideShowIds = ["dash-menu-click", "new-project-menu-click", "ui-element-menu-click", "chart-menu-click","table-menu-click"];
document.addEventListener("DOMContentLoaded", function (event) {
    // Your code to run since DOM is loaded and ready
    for (let i = 0; i < divsForHideShow.length; i++) {
        document.getElementById(divsForHideShow[i]).style.display = "none";
    }

});
function toggleSection(sectionId, menuLink) {
    for (let i = 0; i < divsForHideShow.length; i++) {
        if (menuLink.id.includes(divsForHideShow[i])) {
            document.getElementById(menuLink.id).className = "active";
            document.getElementById(divsForHideShow[i]).style.display = "block";
        } else {
            document.getElementById(divsForHideShowIds[i]).className = "";
            document.getElementById(divsForHideShow[i]).style.display = "none";
        }
    }
}
function EditProject(id) {
    fetch(`/Dashboard/GetProjectById?id=${id}`,
    {
        method: "GET"
    })
        .then(
            response => response.json()
        )
        .then(data => {

            if (data.response.returnCode == 200) {
                EditResponse(data.response.data);
            }
            else
                sweetalert("Error", data.response.returnMsg);
           
        })
        .catch(error => {
    
            sweetalert("Error", "Something went wrong !")
        });
}

function ClickBtnById(id) {
    var linkElement = document.getElementById(id);

    // Create a new click event
    var clickEvent = new MouseEvent("click", {
        bubbles: true,
        cancelable: true,
        view: window
    });

    // Dispatch the click event on the anchor element
    linkElement.dispatchEvent(clickEvent);
}
function EditResponse(response) {
    $("#Spoc").val(response.spoc);
    $("#ProjectName").val(response.projectName);
    $("#IblPriority").val(response.iblPriority);
    $("#CurrentPhase").val(response.currentPhase);
    $("#Status").val(response.status);
    $("#NextPhase").val(response.nextPhase);
    $("#RagStatus").val(response.ragStatus);
    $("#DevStartDate").val(response.devStartDate);
    $("#UatReleaseDate").val(response.uatReleaseDate);
    $("#UatSignoffDate").val(response.uatSignoffDate);
    $("#PreProdReleaseDate").val(response.preProdReleaseDate);
    $("#PreProdSignoffDate").val(response.preProdSignoffDate);
    $("#ProdReleaseDate").val(response.prodReleaseDate);
    $("#IsActive").val(response.isActive);
    $("#ProjectId").val(response.id);
    CKEDITOR.instances.CrDetailsArea.setData(response.crDetails);
    CKEDITOR.instances.CurrentProgressArea.setData(response.currentProgress);
    $("#Project").val(response.environment);
    $('#Project option:selected').attr("hidden-attr", response.projectMasterId);
    if (response.isActive)
        $('input[name="IsActive"][value="Yes"]').prop('checked', true);
    else 
        $('input[name="IsActive"][value="No"]').prop('checked', true);
    let selectedOption = response.environment.trim().toLowerCase();
    if (selectedOption.includes('dev')) {
        $('#DevStartDateSection').css({
            'display': 'flex'
        });
    }
    else {
        $('#DevStartDateSection').css({
            'display': 'none'
        });
    }
    if (selectedOption.includes('uat')) {
        $('#UatReleaseDateSection').css({
            'display': 'flex'
        });
        $('#UatSignoffDateSection').css({
            'display': 'flex'
        });
    }
    else {
        $('#UatReleaseDateSection').css({
            'display': 'none'
        });
        $('#UatSignoffDateSection').css({
            'display': 'none'
        });
    }
    if (selectedOption.includes('pre')) {
        $('#PreProdReleaseDateSection').css({
            'display': 'flex'
        });
        $('#PreProdSignoffDateSection').css({
            'display': 'flex'
        });
    }
    else {
        $('#PreProdReleaseDateSection').css({
            'display': 'none'
        });
        $('#PreProdSignoffDateSection').css({
            'display': 'none'
        });
    }
    if (selectedOption.includes('prod')) {
        $('#ProdReleaseDateSection').css({
            'display': 'flex'
        });
    }
    else {
        $('#ProdReleaseDateSection').css({
            'display': 'none'
        });
    }
    $('#ProjectName').prop('disabled', false);
    $('#Spoc').prop('disabled', false);
    $('#IblPriority').prop('disabled', false);
    $('#CurrentPhase').prop('disabled', false);
    $('#Status').prop('disabled', false);
    $('#NextPhase').prop('disabled', false);
    $('#RagStatus').prop('disabled', false);
    $('#UpdateHeader').css({
        'display': 'block'
    });
    $('#AddHeader').css({
        'display': 'none'
    });
    ClickBtnById("new-project-menu-click");

}

function clearForm() {
    $("#ProjectId").val("");
    $('#Project').val("");

    $('input[name="IsActive"][value="Yes"]').prop('checked', true);
    $("input[name='ProjectName']").val("");
        $("input[name='Spoc']").val("");
    $("input[name='IblPriority']").val("");
    $("input[name='CurrentPhase']").val("");
    $("input[name='Status']").val("");
    $("input[name='NextPhase']").val("");
    CKEDITOR.instances.CrDetailsArea.setData('');
    $("#RagStatus").val("");
    CKEDITOR.instances.CurrentProgressArea.setData('');
        $("input[name='DevStartDate']").val("");
    $("input[name='UatReleaseDate']").val("");
    $("input[name='UatSignoffDate']").val("");
    $("input[name='PreProdReleaseDate']").val("");
    $("input[name='PreProdSignoffDate']").val("");
    $("input[name='ProdReleaseDate']").val("");
    /*mandatoryProjectSelection(true);*/

    $('#ProjectName').prop('disabled', true);
    $('#Spoc').prop('disabled', true);
    $('#IblPriority').prop('disabled', true);
    $('#CurrentPhase').prop('disabled', true);
    $('#Status').prop('disabled', true);
    $('#NextPhase').prop('disabled', true);
    $('#RagStatus').prop('disabled', true);
    $('#DevStartDateSection').css({
        'display': 'none'
    });
    $('#UatReleaseDateSection').css({
        'display': 'none'
    });
    $('#UatSignoffDateSection').css({
        'display': 'none'
    });
    $('#PreProdReleaseDateSection').css({
        'display': 'none'
    });
    $('#PreProdSignoffDateSection').css({
        'display': 'none'
    });
    $('#ProdReleaseDateSection').css({
        'display': 'none'
    });
    $('#UpdateHeader').css({
        'display': 'none'
    });
    $('#AddHeader').css({
        'display': 'block'
    });
}
