//Common Messagebox 
function commonMsgBox(msg) {
    try {
        alert(msg);
    }
    catch (exception) {
        return;
    }
    finally {
        return;
    }

}


function allow_only_numbers(e) {
    try {

        var kCode;
        if (e.keyCode) {
            kCode = e.keyCode;

        }
        else {
            kCode = e.charCode;
           
        }
        if (kCode == 37 || kCode == 46 || kCode == 39 || kCode == 35 || kCode == 36 || kCode == 38) {
            e.preventDefault();
            return;
        }
        var key_codes = [48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 8, 37, 39, 46, 35, 36, 9];

        if (!($.inArray(kCode, key_codes) >= 0)) {
            e.preventDefault();

        }

    }
    catch (ex) {

    }
}

function allow_only_numbers_text(e) {
    try{
        var regex = new RegExp("^[a-zA-Z0-9]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }

        e.preventDefault();
        return false;
    }
    catch (ex) {

    }
}


function allow_only_Float(e, cntrl) {
    try {
        var tChkDecimal = $("#" + cntrl).val();

        var kCode;
        if (e.keyCode) {
            kCode = e.keyCode;
        }
        else {
            kCode = e.charCode;
            if (kCode == 37 || kCode == 39 || kCode == 35 || kCode == 36 || kCode == 38) {
                e.preventDefault();
                return;
            }
        }
        if (kCode == 46) { if ((tChkDecimal.split(".").length) > 1) { e.preventDefault(); return; } }
        var key_codes = [46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 8, 37, 39, 46, 35, 36, 9];
        if (!($.inArray(kCode, key_codes) >= 0)) {
            e.preventDefault();
            return;
        }

    }
    catch (ex) {

    }
}

function allow_only_Float_tax(e, cntrl) {
    try {
        var tChkDecimal = $("#" + cntrl).val();

        var kCode;
        if (e.keyCode) {
            kCode = e.keyCode;
        }
        else {
            kCode = e.charCode;
            if (kCode == 37 || kCode == 39 || kCode == 35 || kCode == 36 || kCode == 38) {
                e.preventDefault();
                return;
            }
        }
        if (tChkDecimal != null && tChkDecimal != "") {
            if (parseFloat(tChkDecimal) > 99 && kCode != 8 && kCode != 46 && kCode != 37 && kCode != 39)
            { e.preventDefault(); $("#" + cntrl).val(tChkDecimal.substring(0, tChkDecimal.length - 1)); return; }
        }
        if (kCode == 46) { if ((tChkDecimal.split(".").length) > 1) { e.preventDefault(); return; } }
        var key_codes = [46, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 8, 37, 39, 46, 35, 36, 9];
        if (!($.inArray(kCode, key_codes) >= 0)) {
            e.preventDefault();
            return;
        }

    }
    catch (ex) {

    }
}


function allow_only_alphabets_name(e) {
    try {
        var kCode;
        if (e.keyCode) {
            kCode = e.keyCode;

        }
        else {
            kCode = e.charCode;
            if (kCode == 37 || kCode == 46 || kCode == 39 || kCode == 35 || kCode == 36 || kCode == 38) {
                e.preventDefault();
                return;
            }
        }

        var key_codes = [65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 32, 46, 8, 9, 37, 39, 40, 46, 35, 36];
        if (!($.inArray(kCode, key_codes) >= 0)) {
            e.preventDefault();
        }

    }
    catch (ex) {
        //commonMsgBox(ex.toString);
    }
}



//regular exp value based validate Mobile numbers
function validateNumbers(numbers) {
    try{
        var sum = 0;     
      
        var no = parseInt(numbers);
        while (no > 0) {
            sum = sum + no % 10;
            no = Math.floor(no / 10);
        }
        //  alert("Sum of digits  "+sum);
        if (sum == 0) {
            return false;
        }
        var filter = /^[0-9]+$/;
        return filter.test(numbers);
    }
    catch (ex)
    {
        alert(ex);
    }
}

//regular exp value based validate Email
function validateEmail(email) {
    var re = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

    return re.test(email);
}

//on change image validate
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        //reader.onload = function (e) {
        //    $('#emp_img').attr('src', e.target.result);
        //    $('#EMAddImag').attr('src', e.target.result);
        //}

        reader.readAsDataURL(input.files[0]);
    }
}

$(".imagefile").change(function () {
    readURL(this);
    var val = $(this).val();
   
    switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
        case 'gif': case 'jpg': case 'png': case 'jpeg': case 'GIF' : case 'JPG': case 'PNG': case 'JPEG':
            break;
        default:
            $(this).val('');
            // error message here
            commonMsgBox("Upload only image format (jpg,jpeg,gif,png)");
            break;
    }
    //alert(this.files[0].size/1000);
    var imagesize = parseFloat(this.files[0].size) / 1000;//kb to mb
    if(imagesize>2000)
    {
        $(this).val('');
        commonMsgBox("Image Size Too Large You Can Upload Image Size Less Than 2MB only!")
    }
});

//Date validate
function checkdate(input) {
    alert(input);
    var validformat = /^\d{2}\/\d{2}\/\d{4}$/ //Basic check for format validity
    var returnval = false
    if (!validformat.test(input.value))
        alert("Invalid Date Format. Please correct and submit again.")
    else { //Detailed check for valid date ranges
        var monthfield = input.value.split("/")[0]
        var dayfield = input.value.split("/")[1]
        var yearfield = input.value.split("/")[2]
        var dayobj = new Date(yearfield, monthfield - 1, dayfield)
        if ((dayobj.getMonth() + 1 != monthfield) || (dayobj.getDate() != dayfield) || (dayobj.getFullYear() != yearfield))
            alert("Invalid Day, Month, or Year range detected. Please correct and submit again.")
        else
            returnval = true
    }
    if (returnval == false) input.select()
    return returnval
}

