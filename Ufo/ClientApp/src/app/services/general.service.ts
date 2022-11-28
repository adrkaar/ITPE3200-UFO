import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class GeneralService {
    showDialog = false;
    showWarningDialog = false;
    showWelcomeDialog = true;
    showLoginDialog = false;
    showNotLoggedInDialog = false;
    isLogedIn = false;
    showLoginButton = true;
    showLogoutButton = false;
    isAdminLogedIn = false;
    constructor() { }
}