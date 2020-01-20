function submitThisForm(formName) {
	var retdata = false;
	//reset error messages and border colors to default
	$('#' + formName).find('span').each(function () { $(this).html('') });
	$('#' + formName).find('input').each(function () { $(this).css('border-color', ''); })
	$('#' + formName).find('select').each(function () { $(this).css('border-color', ''); })
	$.ajax({
		url: $('#' + formName).attr('action'),
		method: "POST",
		global: false,
		cache: false,
		async: false,//The ajax query can't be asynchronous because the function would return a result faster than the action would return data.
		data: $('#' + formName).serialize(),
		success: function (data) {
			(data == null) ? retdata = true : retdata = data;//if null, return true, else rerturn data
		},
		error: function (xhr, httpStatusMessage, customErrorMessage) {
			switch (xhr.status) {
				case 410,
					406,
					409,
					403:
					alert(customErrorMessage);
					break;
				case 400:
					$.each(xhr.responseJSON, function (item, err) {
						$('#' + err.key).css('border-color', 'red');
						$('#' + err.key).on("keyup blur change", function () {
							if ($(this).is('input:text')) {
								if ($(this).val().length > $(this).attr('data-val-minlength-min') && $(this).val().length < $(this).attr('data-val-length-max')) {
									$(this).css('border-color', '');
									$(this).parent().find('span').html('');
								}
								else if ($(this).val().length < $(this).attr('data-val-minlength-min')) {
									$(this).css('border-color', 'red');
									$(this).parent().find('span').html($(this).attr('data-val-minlength'));
								}
								else if ($(this).val().length > $(this).attr('data-val-length-max')) {
									$(this).css('border-color', 'red');
									$(this).parent().find('span').html($(this).attr('data-val-length'));
								}
							}
							else {
								if ($(this).val() != 0) {
									$(this).css('border-color', '');
									$(this).parent().find('span').html('');
								}
								else {
									$(this).css('border-color', 'red');
									$(this).parent().find('span').html($(this).attr('data-val-range'));
								}
							}
						});
						$('#' + err.key).parent().find('span').html(err.errors);
					});
					break;
				default:
					alert("There has been an error saving this transaction.");
					break;
			}
			retdata = false;
		}
	});
	return retdata;
}