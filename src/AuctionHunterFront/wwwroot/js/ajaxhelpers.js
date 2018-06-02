async function ajaxGet(url, action) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "GET",
			url: url,
			success: response => {
				action(response);
				resolve(response);
			}
		});
	});
}

async function ajaxGetAndReplace(url, elementToReplace) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "GET",
			url: url,
			success: response => {
				$(elementToReplace).html(response);
				resolve(response);
			}
		});
	});
}

async function taskDelay(miliseconds) {
	return new Promise((resolve, reject) => {
		setTimeout(() => resolve(), miliseconds);
	});
}
