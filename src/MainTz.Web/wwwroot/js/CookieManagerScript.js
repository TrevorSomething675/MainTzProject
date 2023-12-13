function GetCookie(key) {
    var cookies = document.cookie.split(';');
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();
        if (cookie.indexOf(key + '=') === 0) {
            return cookie.substring(key.length + 1);
        }
    }
}