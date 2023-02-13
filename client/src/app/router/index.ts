import { Checkout } from "../pages/checkout.component";
import { ShopPage } from "../pages/shopPage.component";
import { RouterModule } from "@angular/router";

const routes = [
    { path: "", component: ShopPage },
    { path: "checkout", component: Checkout }
];
const router = RouterModule.forRoot(routes, {
    useHash: false
});

export default router;