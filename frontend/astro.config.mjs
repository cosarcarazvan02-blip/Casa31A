import { defineConfig } from "astro/config";
import react from "@astrojs/react";

export default defineConfig({
  integrations: [react()],
  i18n: {
    defaultLocale: "ro",
    locales: ["ro", "en", "hu"],
    routing: {
      prefixDefaultLocale: true
    }
  }
});
