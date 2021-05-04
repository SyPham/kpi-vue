var AuthPlugin = {

    setToken: function (token, expiration,Menus,user,role) {
        localStorage.setItem('authToken', token);
        localStorage.setItem('authTokenExpiration', expiration);
        localStorage.setItem('Menus', Menus);
        localStorage.setItem('User', user);
        localStorage.setItem("Role", role);

        // localStorage.setItem('Username', Username);
    },

    destroyToken: function () {
        localStorage.removeItem('authToken');
        localStorage.removeItem('authTokenExpiration');
        localStorage.removeItem('Menus');
        localStorage.removeItem('User');
        localStorage.removeItem('Role');

        // localStorage.removeItem('Username');
    },

    getToken: function () {
        var token = localStorage.getItem('authToken');
        var expiration = localStorage.getItem('authTokenExpiration');
        var Menus = localStorage.getItem('Menus');
        var User = localStorage.getItem('User');
        var Role = localStorage.getItem('Role');

        if (!token || !expiration || !Menus || !User  )
            return null;

        if (Date.now() > parseInt(expiration)) {
            this.destroyToken();
            return null;
        } else {
            return token;
        }
    },
    getMenus: function () {
        debugger
        var token = localStorage.getItem('authToken');
        var expiration = localStorage.getItem('authTokenExpiration');
        var Menus = localStorage.getItem('Menus');
        var User = localStorage.getItem('User');
        // var User = localStorage.getItem('Pass');

        if (!token || !expiration || !Menus || !User )
            return null;

        if (Date.now() > parseInt(expiration)) {
            this.destroyToken();
            return null;
        } else {
            return Menus;
        }
    },
    loggedIn: function () {
        if (this.getToken())
            return true;
        else
            return false;
    }
};

export default function(Vue) {
    Vue.auth = AuthPlugin;

    Object.defineProperties(Vue.prototype, {
        $auth: {
            get: function() {
                return Vue.auth;
            }
        }
    })
}