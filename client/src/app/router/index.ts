import { Checkout } from "../pages/checkout.component";
import { ShopPage } from "../pages/shopPage.component";
import { RouterModule } from "@angular/router";
import { LoginPage } from "../pages/loginPage.component";

const routes = [
    { path: "", component: ShopPage },
    { path: "checkout", component: Checkout }
    { path: "login", component: LoginPage}
];
const router = RouterModule.forRoot(routes, {
    useHash: false
});

export default router;