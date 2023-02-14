﻿import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { Store } from "../services/store.service";

@Component({
    selector: "login-page",
    templateUrl: "loginPage.component.html"
})

export class LoginPage {
    constructor(public store: Store, private router: Router) { }

    public creds = {
        username: "",
        password: ""
    }

    public errorMessage = "";

    onLogin() {
        this.store.login(this.creds)
            .subscribe(() => {
                if (this.store.order.items.length > 0) {
                    this.router.navigate(["checkout"]);
                } else {
                    this.router.navigate([""]);
                }
            }, error => {
                console.log(error);
                this.errorMessage = "Failed to login";
            });
    }
    
}