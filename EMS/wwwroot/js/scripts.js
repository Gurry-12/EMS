//Load all data from database

$(document).ready(function () {

    GetAllData();

});

function DeleteStudent(id) {

    console.log("Function Called with ID:", id);
    //$(`tr[data-id=${id}]`).remove();

    $.ajax({
        url: `Home/Delete/${id}`,
        type: 'GET',
        success: function (data) {
            $(`tr[data-id=${id}]`).remove();
        },
        error: function (xhr) {
            console.error("Error fetching student data:", xhr.statusText);
        }
    });
}


function UpdateButtonList(id) {
    console.log("Function Called with ID:", id);
    $("#SaveButton").hide();
    $("#UpdateButton").show();
    $("#staticBackdropLabel").text("Update Student Information");

    $.ajax({
        url: `Home/GetById/${id}`,
        type: 'GET',
        success: function (data) {
            //console.log("Student Data:", data);
            $('#studentid').val(data.studentId);
            $('#Name').val(data.studentName);
            $('#standard').val(data.studentStandard);
            $('#roll').val(data.rollNo);
            $('#Address').val(data.studentAddress);
            $('#fees').val(data.fees);
        },
        error: function (xhr) {
            console.error("Error fetching student data:", xhr.statusText);
        }
    });
}
// Form for new Student
$("#NewStudent").click(function () {
    $("#UpdateButton").hide();
    $("#SaveButton").show();
    $("#staticBackdropLabel").text("Add New Student Information");
});

// Ajax for the Save Data
function SaveData() {
    
   
    var data = GetDataByForm(); // Get form data

    console.log("Sending Data:", data);

    $.ajax({
        url: "Home/SaveData",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            console.log("Response:", response);



            // Stop execution if saving failed
            if (!response.data) {
                alert("Something is Missing");

            }
            else {

                console.log("Data Saved Successfully");

                // Reset the form after successful saving
                $('#staticBackdrop').modal('hide');
                $("#studentForm")[0].reset();

                // Clear dynamically added elements
                DeleteChilds();

                // Refresh the data
                GetAllData();

            }
        },
        error: function (xhr, status, error) {
            console.log("AJAX Error:", error);
            alert("An error occurred while saving data. Please try again.");
        }
    });
}


//update
$("#UpdateButton").click(function () {

   
    
    updateData = GetDataByForm();

    console.log(updateData);

    $.ajax({
        url: "Home/UpdateData",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(updateData),
        success: function (response) {

            if (!response.checkStudent) {
                alert("Something is Missing");
            }
            else {
                $('#staticBackdrop').modal('hide');
                $("#studentForm")[0].reset();
                //update row 
                UpdateRowData(response);
                alert(response.messege);
                }

        },
        error: function (response, request, status, error) {
            alert(response.messege);
        }
    });
});

function GetAllData() {

    $.ajax({
        url: 'Home/GetAllData',
        type: 'GET',
        success: function (students) {

            //console.log(student);
            students.forEach(student => {
                const table = `<tr data-id="${student.studentId}">
                <td class="fixed-col">${student.studentId}</td>
                <td>${student.studentName}</td>
                <td>${student.studentStandard}</td>
                <td>${student.rollNo}</td>
                <td>${student.studentAddress}</td>
                <td>${student.fees}</td>
               <td>
                    <div class="btn-group gap-3 " role="group" >
                        <button class="btn btn-outline-warning rounded-1" data-bs-target="#staticBackdrop" data-bs-toggle="modal" onclick="UpdateButtonList(${student.studentId})"><i class="fa-solid fa-pen"></i></button>
                        <button class="btn btn-outline-danger rounded-1" onclick="DeleteStudent(${student.studentId})"><i class="fa-solid fa-user-xmark"></i></button>
                    </div>
                </td>
            </tr>`;
                $("#studentTableBody").append(table);
            });


        },

        error: function (xhr) {
            console.error("Error fetching student data:", xhr.statusText);
        }
    });
}

function DeleteChilds() {
    $("tbody").empty();
}


function UpdateRowData(response) {
    //console.log(response.checkStudent);

    // Find the specific row using data-id
    const row = $(`tr[data-id=${response.checkStudent.studentId}]`);
    //console.log(row)
    if (row) {
        row.find("td").eq(1).text(response.checkStudent.studentName);
        row.find("td").eq(2).text(response.checkStudent.studentStandard);
        row.find("td").eq(3).text(response.checkStudent.rollNo);
        row.find("td").eq(4).text(response.checkStudent.studentAddress);
        row.find("td").eq(5).text(response.checkStudent.fees);
    }
    else {
        console.log("Please check the inputs");
    }
}

function GetDataByForm() {


    const data = {
        StudentId: Number($("#StudentId").val()),
        StudentName: $("#StudentName").val(),
        StudentStandard: Number($("#StudentStandard").val()),
        RollNo: Number($("#RollNo").val()),
        StudentAddress: $("#StudentAddress").val(),
        Fees: Number($("#Fees").val())
    };
    console.log("Collected Data:", data);  // De
    return data;
}