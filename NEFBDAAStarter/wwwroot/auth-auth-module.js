(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["auth-auth-module"],{

/***/ "./src/app/auth/auth-routing.module.ts":
/*!*********************************************!*\
  !*** ./src/app/auth/auth-routing.module.ts ***!
  \*********************************************/
/*! exports provided: routes, NgxAuthRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "routes", function() { return routes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NgxAuthRoutingModule", function() { return NgxAuthRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _nebular_auth__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @nebular/auth */ "./node_modules/@nebular/auth/index.js");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./login/login.component */ "./src/app/auth/login/login.component.ts");
/* harmony import */ var _register_register_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./register/register.component */ "./src/app/auth/register/register.component.ts");
/* harmony import */ var _reset_reset_password_component__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./reset/reset-password.component */ "./src/app/auth/reset/reset-password.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var routes = [
    {
        path: '',
        component: _nebular_auth__WEBPACK_IMPORTED_MODULE_2__["NbAuthComponent"],
        children: [
            {
                path: '',
                redirectTo: 'login',
            },
            {
                path: 'login',
                component: _login_login_component__WEBPACK_IMPORTED_MODULE_3__["NgxLoginComponent"],
            },
            {
                path: 'register',
                component: _register_register_component__WEBPACK_IMPORTED_MODULE_4__["NgxRegisterComponent"],
            },
            {
                path: 'logout',
                component: _nebular_auth__WEBPACK_IMPORTED_MODULE_2__["NbLogoutComponent"],
            },
            {
                path: 'request-password',
                component: _reset_reset_password_component__WEBPACK_IMPORTED_MODULE_5__["NgxResetPasswordComponent"],
            },
        ],
    },
];
var NgxAuthRoutingModule = /** @class */ (function () {
    function NgxAuthRoutingModule() {
    }
    NgxAuthRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]],
        })
    ], NgxAuthRoutingModule);
    return NgxAuthRoutingModule;
}());



/***/ }),

/***/ "./src/app/auth/auth.module.ts":
/*!*************************************!*\
  !*** ./src/app/auth/auth.module.ts ***!
  \*************************************/
/*! exports provided: NgxAuthModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NgxAuthModule", function() { return NgxAuthModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _auth_routing_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./auth-routing.module */ "./src/app/auth/auth-routing.module.ts");
/* harmony import */ var _nebular_auth__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @nebular/auth */ "./node_modules/@nebular/auth/index.js");
/* harmony import */ var _nebular_theme__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @nebular/theme */ "./node_modules/@nebular/theme/index.js");
/* harmony import */ var _login_login_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./login/login.component */ "./src/app/auth/login/login.component.ts");
/* harmony import */ var _register_register_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./register/register.component */ "./src/app/auth/register/register.component.ts");
/* harmony import */ var _reset_reset_password_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./reset/reset-password.component */ "./src/app/auth/reset/reset-password.component.ts");
/* harmony import */ var _theme_theme_module__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ../@theme/theme.module */ "./src/app/@theme/theme.module.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};











var NgxAuthModule = /** @class */ (function () {
    function NgxAuthModule() {
    }
    NgxAuthModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            imports: [
                _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_3__["RouterModule"],
                _nebular_theme__WEBPACK_IMPORTED_MODULE_6__["NbAlertModule"],
                _nebular_theme__WEBPACK_IMPORTED_MODULE_6__["NbCheckboxModule"],
                _auth_routing_module__WEBPACK_IMPORTED_MODULE_4__["NgxAuthRoutingModule"],
                _nebular_auth__WEBPACK_IMPORTED_MODULE_5__["NbAuthModule"],
                _theme_theme_module__WEBPACK_IMPORTED_MODULE_10__["ThemeModule"],
            ],
            declarations: [
                _login_login_component__WEBPACK_IMPORTED_MODULE_7__["NgxLoginComponent"],
                _register_register_component__WEBPACK_IMPORTED_MODULE_8__["NgxRegisterComponent"],
                _reset_reset_password_component__WEBPACK_IMPORTED_MODULE_9__["NgxResetPasswordComponent"],
            ],
        })
    ], NgxAuthModule);
    return NgxAuthModule;
}());



/***/ }),

/***/ "./src/app/auth/login/login.component.html":
/*!*************************************************!*\
  !*** ./src/app/auth/login/login.component.html ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h1 id=\"title\" class=\"title\">Login</h1>\r\n<p class=\"sub-title\">Hello! Log in with your email.</p>\r\n\r\n<nb-alert *ngIf=\"showMessages.error && errors?.length && !submitted\" outline=\"danger\" role=\"alert\">\r\n  <p class=\"alert-title\"><b>Oh snap!</b></p>\r\n  <ul class=\"alert-message-list\">\r\n    <li *ngFor=\"let error of errors\" class=\"alert-message\">{{ error }}</li>\r\n  </ul>\r\n</nb-alert>\r\n\r\n<nb-alert *ngIf=\"showMessages.success && messages?.length && !submitted\" outline=\"success\" role=\"alert\">\r\n  <p class=\"alert-title\"><b>Hooray!</b></p>\r\n  <ul class=\"alert-message-list\">\r\n    <li *ngFor=\"let message of messages\" class=\"alert-message\">{{ message }}</li>\r\n  </ul>\r\n</nb-alert>\r\n\r\n<form (ngSubmit)=\"login()\" #form=\"ngForm\" aria-labelledby=\"title\">\r\n\r\n  <div class=\"form-control-group\">\r\n    <label class=\"label\" for=\"input-email\">Email address:</label>\r\n    <input nbInput\r\n           fullWidth\r\n           [(ngModel)]=\"user.email\"\r\n           #email=\"ngModel\"\r\n           name=\"email\"\r\n           id=\"input-email\"\r\n           pattern=\".+@.+\\..+\"\r\n           placeholder=\"Email address\"\r\n           autofocus\r\n           [status]=\"email.dirty ? (email.invalid  ? 'danger' : 'success') : ''\"\r\n           [required]=\"getConfigValue('forms.validation.email.required')\"\r\n           [attr.aria-invalid]=\"email.invalid && email.touched ? true : null\">\r\n    <ng-container *ngIf=\"email.invalid && email.touched\">\r\n      <p class=\"error-message\" *ngIf=\"email.errors?.required\">\r\n        Email is required!\r\n      </p>\r\n      <p class=\"error-message\" *ngIf=\"email.errors?.pattern\">\r\n        Email should be the real one!\r\n      </p>\r\n    </ng-container>\r\n  </div>\r\n\r\n  <div class=\"form-control-group\">\r\n    <label class=\"label\" for=\"input-password\">Password:</label>\r\n    <input nbInput\r\n           fullWidth\r\n           [(ngModel)]=\"user.password\"\r\n           #password=\"ngModel\"\r\n           name=\"password\"\r\n           type=\"password\"\r\n           id=\"input-password\"\r\n           placeholder=\"Password\"\r\n           [status]=\"password.dirty ? (password.invalid  ? 'danger' : 'success') : ''\"\r\n           [required]=\"getConfigValue('forms.validation.password.required')\"\r\n           [minlength]=\"getConfigValue('forms.validation.password.minLength')\"\r\n           [maxlength]=\"getConfigValue('forms.validation.password.maxLength')\"\r\n           [attr.aria-invalid]=\"password.invalid && password.touched ? true : null\">\r\n    <ng-container *ngIf=\"password.invalid && password.touched \">\r\n      <p class=\"error-message\" *ngIf=\"password.errors?.required\">\r\n        Password is required!\r\n      </p>\r\n      <p class=\"error-message\" *ngIf=\"password.errors?.minlength || password.errors?.maxlength\">\r\n        Password should contains\r\n        from {{ getConfigValue('forms.validation.password.minLength') }}\r\n        to {{ getConfigValue('forms.validation.password.maxLength') }}\r\n        characters\r\n      </p>\r\n    </ng-container>\r\n  </div>\r\n\r\n  <div class=\"form-control-group accept-group\">\r\n    <nb-checkbox name=\"rememberMe\" [(ngModel)]=\"user.rememberMe\" *ngIf=\"rememberMe\">Remember me</nb-checkbox>\r\n    <a class=\"forgot-password\" routerLink=\"../request-password\">Forgot Password?</a>\r\n  </div>\r\n\r\n  <button nbButton\r\n          fullWidth\r\n          status=\"success\"\r\n          [disabled]=\"submitted || !form.valid\"\r\n          [class.btn-pulse]=\"submitted\">\r\n    Log In\r\n  </button>\r\n</form>\r\n\r\n<section *ngIf=\"socialLinks && socialLinks.length > 0\" class=\"links\" aria-label=\"Social sign in\">\r\n  or enter with:\r\n  <div class=\"socials\">\r\n    <ng-container *ngFor=\"let socialLink of socialLinks\">\r\n      <a *ngIf=\"socialLink.link\"\r\n         [routerLink]=\"socialLink.link\"\r\n         [attr.target]=\"socialLink.target\"\r\n         [attr.class]=\"socialLink.icon\"\r\n         [class.with-icon]=\"socialLink.icon\">{{ socialLink.title }}</a>\r\n      <a *ngIf=\"socialLink.url\"\r\n         [attr.href]=\"socialLink.url\"\r\n         [attr.target]=\"socialLink.target\"\r\n         [attr.class]=\"socialLink.icon\"\r\n         [class.with-icon]=\"socialLink.icon\">{{ socialLink.title }}</a>\r\n    </ng-container>\r\n  </div>\r\n</section>\r\n\r\n<section class=\"another-action\" aria-label=\"Register\">\r\n  Don't have an account? <a class=\"text-link\" routerLink=\"../register\">Register</a>\r\n</section>\r\n"

/***/ }),

/***/ "./src/app/auth/login/login.component.ts":
/*!***********************************************!*\
  !*** ./src/app/auth/login/login.component.ts ***!
  \***********************************************/
/*! exports provided: NgxLoginComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NgxLoginComponent", function() { return NgxLoginComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _nebular_auth__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @nebular/auth */ "./node_modules/@nebular/auth/index.js");
/* harmony import */ var _utils_utils__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../utils/utils */ "./src/app/utils/utils.ts");
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var NgxLoginComponent = /** @class */ (function (_super) {
    __extends(NgxLoginComponent, _super);
    function NgxLoginComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    NgxLoginComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.onTokenChange().subscribe(function (token) {
            if (token.isValid()) {
                var returnUrl_1 = Object(_utils_utils__WEBPACK_IMPORTED_MODULE_2__["getParamValueQueryString"])('returnUrl') || '/pages/dashboard';
                // TODO jak to lepie zrobic
                setTimeout(function () { _this.router.navigate([returnUrl_1]); }, 1200);
            }
        });
    };
    NgxLoginComponent.prototype.onChange = function (event) {
        if (this.phoneNumberCode && this.phoneNumber) {
            this.user.loginName = String(this.phoneNumberCode) + String(this.phoneNumber);
        }
    };
    NgxLoginComponent.prototype.errorsOccurred = function () {
        return this.showMessages.error && this.errors && (this.errors.length) > 0 && (!this.submitted);
    };
    NgxLoginComponent.prototype.loginSucceeded = function () {
        return this.showMessages.success && this.messages && this.messages.length > 0 && !this.submitted;
    };
    NgxLoginComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'ngx-login',
            template: __webpack_require__(/*! ./login.component.html */ "./src/app/auth/login/login.component.html"),
        })
    ], NgxLoginComponent);
    return NgxLoginComponent;
}(_nebular_auth__WEBPACK_IMPORTED_MODULE_1__["NbLoginComponent"]));



/***/ }),

/***/ "./src/app/auth/register/register.component.html":
/*!*******************************************************!*\
  !*** ./src/app/auth/register/register.component.html ***!
  \*******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<nb-auth-block>\r\n  <h2 class=\"title\">Sign Up</h2>\r\n  <form (ngSubmit)=\"register()\" #form=\"ngForm\" name=\"form\">\r\n    <div *ngIf=\"showMessages.error && errors && errors.length > 0 && !submitted\"\r\n         class=\"alert alert-danger\" role=\"alert\">\r\n      <div *ngFor=\"let error of errors\">{{ error }}</div>\r\n    </div>\r\n    <div *ngIf=\"showMessages.success && messages && messages.length > 0 && !submitted\"\r\n         class=\"alert alert-success\" role=\"alert\">\r\n      <div *ngFor=\"let message of messages\">{{ message }}</div>\r\n    </div>\r\n\r\n    <!--FIRST NAME-->\r\n    <div class=\"form-group\">\r\n      <label for=\"firstName\" class=\"sr-only\">First name</label>\r\n      <input nbInput name=\"firstName\" [(ngModel)]=\"user.firstName\" id=\"firstName\" #firstName=\"ngModel\"\r\n             class=\"form-control\" placeholder=\"First name*\"\r\n             [class.form-control-danger]=\"firstName.invalid && firstName.touched\"\r\n             [required]=\"getConfigValue('forms.validation.firstName.required')\"\r\n             [minlength]=\"getConfigValue('forms.validation.firstName.minLength')\"\r\n             [maxlength]=\"getConfigValue('forms.validation.firstName.maxLength')\"\r\n             autofocus>\r\n      <p class=\"error-message\" *ngIf=\"firstName.invalid && firstName.touched && firstName.errors?.required\">\r\n        First name is required!\r\n      </p>\r\n      <p\r\n        class=\"error-message\"\r\n        *ngIf=\"firstName.invalid && firstName.touched && (firstName.errors?.minlength || firstName.errors?.maxlength)\">\r\n        First name should contains\r\n        from {{getConfigValue('forms.validation.firstName.minLength')}}\r\n        to {{getConfigValue('forms.validation.firstName.maxLength')}}\r\n        characters\r\n      </p>\r\n    </div>\r\n\r\n    <!--LAST NAME-->\r\n    <div class=\"form-group\">\r\n      <label for=\"lastName\" class=\"sr-only\">Last name</label>\r\n      <input nbInput name=\"lastName\" [(ngModel)]=\"user.lastName\" id=\"lastName\" #lastName=\"ngModel\"\r\n             class=\"form-control\" placeholder=\"Last name*\"\r\n             [class.form-control-danger]=\"lastName.invalid && lastName.touched\"\r\n             [required]=\"getConfigValue('forms.validation.lastName.required')\"\r\n             [minlength]=\"getConfigValue('forms.validation.lastName.minLength')\"\r\n             [maxlength]=\"getConfigValue('forms.validation.lastName.maxLength')\"\r\n             autofocus>\r\n      <p class=\"error-message\" *ngIf=\"lastName.invalid && lastName.touched && lastName.errors?.required\">\r\n        Last name is required!\r\n      </p>\r\n      <p\r\n        class=\"error-message\"\r\n        *ngIf=\"lastName.invalid && lastName.touched && (lastName.errors?.minlength || lastName.errors?.maxlength)\">\r\n        Last name should contains\r\n        from {{getConfigValue('forms.validation.lastName.minLength')}}\r\n        to {{getConfigValue('forms.validation.lastName.maxLength')}}\r\n        characters\r\n      </p>\r\n    </div>\r\n\r\n    \r\n\r\n    <!--EMAIL-->\r\n    <div class=\"form-group\">\r\n      <label for=\"input-email\" class=\"sr-only\">Email address</label>\r\n      <input nbInput name=\"email\" [(ngModel)]=\"user.email\" id=\"input-email\" #email=\"ngModel\"\r\n             class=\"form-control\" placeholder=\"Email address*\" pattern=\".+@.+\\..+\"\r\n             [class.form-control-danger]=\"email.invalid && email.touched\"\r\n             [required]=\"getConfigValue('forms.validation.email.required')\">\r\n      <p class=\"error-message\" *ngIf=\"email.invalid && email.touched && email.errors?.required\">\r\n        Email is required!\r\n      </p>\r\n      <p class=\"error-message\"\r\n         *ngIf=\"email.invalid && email.touched && email.errors?.pattern\">\r\n        Email should be the real one!\r\n      </p>\r\n    </div>\r\n\r\n\r\n    <!--PASSWORD-->\r\n    <div class=\"form-group\">\r\n      <label for=\"input-password\" class=\"sr-only\">Password</label>\r\n      <input nbInput name=\"password\" [(ngModel)]=\"user.password\" type=\"password\" id=\"input-password\"\r\n             class=\"form-control\" placeholder=\"Password*\" #password=\"ngModel\"\r\n             [class.form-control-danger]=\"password.invalid && password.touched\"\r\n             [required]=\"getConfigValue('forms.validation.password.required')\"\r\n             [minlength]=\"getConfigValue('forms.validation.password.minLength')\"\r\n             [maxlength]=\"getConfigValue('forms.validation.password.maxLength')\">\r\n      <p class=\"error-message\" *ngIf=\"password.invalid && password.touched && password.errors?.required\">\r\n        Password is required!\r\n      </p>\r\n      <p\r\n        class=\"error-message\"\r\n        *ngIf=\"password.invalid && password.touched && (password.errors?.minlength || password.errors?.maxlength)\">\r\n        Password should contains\r\n        from {{ getConfigValue('forms.validation.password.minLength') }}\r\n        to {{ getConfigValue('forms.validation.password.maxLength') }}\r\n        characters\r\n      </p>\r\n    </div>\r\n\r\n    <!--REPASSWORD-->\r\n    <div class=\"form-group\">\r\n      <label for=\"input-re-password\" class=\"sr-only\">Repeat password</label>\r\n      <input nbInput\r\n             name=\"rePass\" [(ngModel)]=\"user.confirmPassword\" type=\"password\" id=\"input-re-password\"\r\n             class=\"form-control\" placeholder=\"Confirm Password*\" #rePass=\"ngModel\"\r\n             [class.form-control-danger]=\"(rePass.invalid || password.value != rePass.value) && rePass.touched\"\r\n             [required]=\"getConfigValue('forms.validation.password.required')\">\r\n      <p class=\"error-message\"\r\n         *ngIf=\"rePass.invalid && rePass.touched && rePass.errors?.required\">\r\n        Password confirmation is required!\r\n      </p>\r\n      <p\r\n        class=\"error-message\"\r\n        *ngIf=\"rePass.touched && password.value != rePass.value && !rePass.errors?.required\">\r\n        Password does not match the confirm password.\r\n      </p>\r\n    </div>\r\n    <button nbButton class=\"btn-block btn-success\" [disabled]=\"submitted || !form.valid\" [class.btn-pulse]=\"submitted\">\r\n      Register\r\n    </button>\r\n  </form>\r\n  <div class=\"links\">\r\n    <ng-container *ngIf=\"socialLinks && socialLinks.length > 0\">\r\n      <p class=\"form-text\">Or connect with:</p>\r\n      <div class=\"socials\">\r\n        <ng-container *ngFor=\"let socialLink of socialLinks\">\r\n          <a *ngIf=\"socialLink.link\"\r\n             [routerLink]=\"socialLink.link\"\r\n             [attr.target]=\"socialLink.target\"\r\n             [attr.class]=\"socialLink.icon\"\r\n             [class.with-icon]=\"socialLink.icon\">{{ socialLink.title }}</a>\r\n          <a *ngIf=\"socialLink.url\"\r\n             [attr.href]=\"socialLink.url\"\r\n             [attr.target]=\"socialLink.target\"\r\n             [attr.class]=\"socialLink.icon\"\r\n             [class.with-icon]=\"socialLink.icon\">{{ socialLink.title }}</a>\r\n        </ng-container>\r\n      </div>\r\n    </ng-container>\r\n    <p class=\"form-text\">\r\n      Already have an account? <a routerLink=\"../login\"><strong>Sign in</strong></a>\r\n    </p>\r\n  </div>\r\n</nb-auth-block>\r\n"

/***/ }),

/***/ "./src/app/auth/register/register.component.ts":
/*!*****************************************************!*\
  !*** ./src/app/auth/register/register.component.ts ***!
  \*****************************************************/
/*! exports provided: NgxRegisterComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NgxRegisterComponent", function() { return NgxRegisterComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _nebular_auth__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @nebular/auth */ "./node_modules/@nebular/auth/index.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (undefined && undefined.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};



var NgxRegisterComponent = /** @class */ (function (_super) {
    __extends(NgxRegisterComponent, _super);
    function NgxRegisterComponent(service, options, cd, router) {
        if (options === void 0) { options = {}; }
        var _this = _super.call(this, service, options, cd, router) || this;
        _this.service = service;
        _this.options = options;
        _this.cd = cd;
        _this.router = router;
        return _this;
    }
    NgxRegisterComponent.prototype.ngOnInit = function () {
    };
    NgxRegisterComponent.prototype.onChange = function (event) {
        if (this.phoneNumber && this.phoneNumberCode) {
            this.user.phoneNumber = String(this.phoneNumberCode) + String(this.phoneNumber);
        }
    };
    NgxRegisterComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'ngx-register',
            template: __webpack_require__(/*! ./register.component.html */ "./src/app/auth/register/register.component.html"),
        }),
        __param(1, Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Inject"])(_nebular_auth__WEBPACK_IMPORTED_MODULE_1__["NB_AUTH_OPTIONS"])),
        __metadata("design:paramtypes", [_nebular_auth__WEBPACK_IMPORTED_MODULE_1__["NbAuthService"], Object, _angular_core__WEBPACK_IMPORTED_MODULE_0__["ChangeDetectorRef"],
            _angular_router__WEBPACK_IMPORTED_MODULE_2__["Router"]])
    ], NgxRegisterComponent);
    return NgxRegisterComponent;
}(_nebular_auth__WEBPACK_IMPORTED_MODULE_1__["NbRegisterComponent"]));



/***/ }),

/***/ "./src/app/auth/reset/reset-password.component.html":
/*!**********************************************************!*\
  !*** ./src/app/auth/reset/reset-password.component.html ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<nb-auth-block>\r\n  <h2 class=\"title\">Forgot Password</h2>\r\n  <p class=\"sub-title\">If you have forgotten your password, please enter your phone number and press\r\n    \"Reset password\" button. The new password will be sent by SMS\r\n  </p>\r\n  <form (ngSubmit)=\"requestPass()\" #requestPassForm=\"ngForm\">\r\n    <div *ngIf=\"showMessages.error && errors && errors.length > 0 && !submitted\"\r\n         class=\"alert alert-danger\" role=\"alert\">\r\n      <div *ngFor=\"let error of errors\">{{ error }}</div>\r\n    </div>\r\n    <div *ngIf=\"showMessages.success && messages && messages.length > 0 && !submitted\"\r\n         class=\"alert alert-success\" role=\"alert\">\r\n      <div *ngFor=\"let message of messages\">{{ message }}</div>\r\n    </div>\r\n    <div class=\"form-row form-group\">\r\n      <div class=\"col-md-2\">\r\n        <input nbInput name=\"phoneNumberCode\" size=\"2\" [(ngModel)]=\"phoneNumberCode\" (ngModelChange)=\"onChange($event)\"\r\n               id=\"input-phone-number-code\" type=\"tel\" pattern=\"[0-9]{2}\"\r\n               class=\"form-control\" #numberC=\"ngModel\" autocomplete=\"off\"\r\n               [class.form-control-danger]=\"numberC.invalid && numberC.touched\" autofocus\r\n               [required]=\"getConfigValue('forms.validation.number.required')\">\r\n      </div>\r\n      <div class=\"col-md-10\">\r\n        <input nbInput name=\"phoneNumber\" [(ngModel)]=\"phoneNumber\" (ngModelChange)=\"onChange($event)\"\r\n               id=\"input-phone-number\"\r\n               type=\"tel\" pattern=\"[0-9]{9}\"\r\n               class=\"form-control\" placeholder=\"Phone number*\" #number=\"ngModel\"\r\n               [class.form-control-danger]=\"number.invalid && number.touched\"\r\n               [required]=\"getConfigValue('forms.validation.number.required')\">\r\n      </div>\r\n      <p class=\"error-message\"\r\n         *ngIf=\"(number.invalid && number.touched && number.errors?.required)||(numberC.invalid && numberC.touched && numberC.errors?.required)\">\r\n        Phone number is required!\r\n      </p>\r\n      <p class=\"error-message\"\r\n         *ngIf=\"(number.invalid && number.touched && number.errors?.pattern) || (numberC.invalid && numberC.touched && numberC.errors?.pattern)\">\r\n        Phone number should be the real one!\r\n      </p>\r\n    </div>\r\n    <button nbButton class=\"btn-block btn-success\" [disabled]=\"submitted || !requestPassForm.form.valid\"\r\n            [class.btn-pulse]=\"submitted\">\r\n      Reset password\r\n    </button>\r\n  </form>\r\n  <div class=\"links col-sm-12\">\r\n    <p class=\"form-text\">\r\n      Already have an account? <a routerLink=\"../login\"><strong>Sign In</strong></a>\r\n    </p>\r\n    <p class=\"form-text\">\r\n      <a routerLink=\"../register\"><strong>Sign Up</strong></a>\r\n    </p>\r\n  </div>\r\n</nb-auth-block>\r\n"

/***/ }),

/***/ "./src/app/auth/reset/reset-password.component.ts":
/*!********************************************************!*\
  !*** ./src/app/auth/reset/reset-password.component.ts ***!
  \********************************************************/
/*! exports provided: NgxResetPasswordComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NgxResetPasswordComponent", function() { return NgxResetPasswordComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _nebular_auth__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @nebular/auth */ "./node_modules/@nebular/auth/index.js");
var __extends = (undefined && undefined.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var NgxResetPasswordComponent = /** @class */ (function (_super) {
    __extends(NgxResetPasswordComponent, _super);
    function NgxResetPasswordComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    NgxResetPasswordComponent.prototype.onChange = function (event) {
        if (this.phoneNumber && this.phoneNumberCode) {
            this.user.loginName = String(this.phoneNumberCode) + String(this.phoneNumber);
        }
    };
    NgxResetPasswordComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'ngx-request-password-page',
            template: __webpack_require__(/*! ./reset-password.component.html */ "./src/app/auth/reset/reset-password.component.html"),
        })
    ], NgxResetPasswordComponent);
    return NgxResetPasswordComponent;
}(_nebular_auth__WEBPACK_IMPORTED_MODULE_1__["NbRequestPasswordComponent"]));



/***/ })

}]);
//# sourceMappingURL=auth-auth-module.js.map