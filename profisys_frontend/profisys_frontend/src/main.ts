import { createApp } from "vue";
import "./style.scss";
import App from "./App.vue";
import PrimeVue from "primevue/config";
import ToastService from "primevue/toastservice";
import ConfirmationService from "primevue/confirmationservice";
import "primevue/resources/themes/aura-light-blue/theme.css";
import "primevue/resources/primevue.min.css";
import "primeicons/primeicons.css";

const app = createApp(App);
app.use(PrimeVue);
app.use(ToastService);
app.use(ConfirmationService);
app.mount("#app");
