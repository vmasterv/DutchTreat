﻿import { Checkout } from "../pages/checkout.component";
import { ShopPage } from "../pages/shopPage.component";
import { RouterModule } from "@angular/router";
import { LoginPage } from "../pages/loginPage.component";
import { AuthActivator } from "../services/authActivator.service";

const routes = [
    { path: "", component: ShopPage },
    { path: "checkout", component: Checkout, canActivate: [AuthActivator] },
    { path: "login", component: LoginPage },
    { path: "**", redirectTo: "/" }
];
const router = RouterModule.forRoot(routes, {
    useHash: false
});

export default router;