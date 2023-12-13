const registerForm = document.querySelector('#RegisterForm');
registerForm.addEventListener('fetch', function (event) {
    //event.preventDefault();
    fetch(registerForm.action, {
        method: registerForm.method,
        body: new FormData(registerForm)
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            } else {
                console.log('Ошибка при получении запроса');
            }
        })
        .then(data => {
            if (data != '') {
                document.cookie = `accessToken=${data.value.accessToken}`;
                document.cookie = `refreshToken=${data.value.refreshToken}`;
                document.cookie = `role=${data.value.role}`;
                console.log(`Access token: ${data.value.accesstoken} Refresh token: ${data.value.refreshtoken} Role: ${data.value.role}`);
                window.location.pathname = `${data.value.role}/index`;
            } else {
                console.log('Данные не найдены в ответе сервера')
            }
        })
        .catch(error => {
            console.error('Произошла ошибка:', error);
        });
});