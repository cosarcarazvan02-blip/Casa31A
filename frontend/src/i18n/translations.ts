export const locales = ["ro", "en", "hu"] as const;
export type Locale = (typeof locales)[number];
export const defaultLocale: Locale = "ro";

export const translations = {
  ro: {
    siteName: "Casa31A",
    tagline: "Pensiune în Borsec",
    nav: { home: "Acasă", rooms: "Camere", gallery: "Galerie", contact: "Contact", book: "Rezervă" },
    home: {
      heroTitle: "Bine ați venit la Casa31A",
      heroSubtitle: "O pensiune primitoare în inima stațiunii Borsec, ideală pentru relaxare și drumeții montane.",
      cta: "Vezi camerele disponibile",
      aboutTitle: "Despre noi",
      aboutBody: "Casa31A este o pensiune de familie situată în Borsec, cunoscută pentru izvoarele sale de apă minerală și aerul curat de munte. Vă oferim camere confortabile, mic dejun tradițional și o atmosferă caldă, ca acasă."
    },
    rooms: {
      title: "Camerele noastre",
      perNight: "/ noapte",
      capacity: "persoane",
      bookNow: "Rezervă această cameră",
      loading: "Se încarcă...",
      loadError: "Nu am putut încărca camerele momentan."
    },
    gallery: { title: "Galerie foto" },
    contact: {
      title: "Contact",
      address: "Borsec, județul Harghita, România",
      formName: "Nume",
      formEmail: "Email",
      formMessage: "Mesaj",
      formSubmit: "Trimite"
    },
    booking: {
      title: "Rezervare",
      checkIn: "Data sosirii",
      checkOut: "Data plecării",
      guests: "Număr de persoane",
      fullName: "Nume complet",
      email: "Email",
      phone: "Telefon",
      submit: "Continuă spre plată",
      totalLabel: "Total de plată",
      nightsWord: "nopți",
      invalidDates: "Data de plecare trebuie să fie după data sosirii.",
      genericError: "A apărut o eroare. Încearcă din nou.",
      networkError: "Nu am putut contacta serverul. Încearcă din nou.",
      successTitle: "Rezervare confirmată!",
      successBody: "Mulțumim! Rezervarea ta a fost înregistrată și plătită cu succes. Te așteptăm la Casa31A.",
      cancelTitle: "Plată anulată",
      cancelBody: "Rezervarea nu a fost finalizată. Poți relua procesul oricând."
    }
  },
  en: {
    siteName: "Casa31A",
    tagline: "Guesthouse in Borsec",
    nav: { home: "Home", rooms: "Rooms", gallery: "Gallery", contact: "Contact", book: "Book now" },
    home: {
      heroTitle: "Welcome to Casa31A",
      heroSubtitle: "A cozy guesthouse in the heart of Borsec, perfect for relaxation and mountain hiking.",
      cta: "See available rooms",
      aboutTitle: "About us",
      aboutBody: "Casa31A is a family-run guesthouse located in Borsec, known for its mineral water springs and clean mountain air. We offer comfortable rooms, traditional breakfast, and a warm, home-like atmosphere."
    },
    rooms: {
      title: "Our rooms",
      perNight: "/ night",
      capacity: "guests",
      bookNow: "Book this room",
      loading: "Loading...",
      loadError: "Could not load rooms right now."
    },
    gallery: { title: "Photo gallery" },
    contact: {
      title: "Contact",
      address: "Borsec, Harghita County, Romania",
      formName: "Name",
      formEmail: "Email",
      formMessage: "Message",
      formSubmit: "Send"
    },
    booking: {
      title: "Booking",
      checkIn: "Check-in date",
      checkOut: "Check-out date",
      guests: "Number of guests",
      fullName: "Full name",
      email: "Email",
      phone: "Phone",
      submit: "Continue to payment",
      totalLabel: "Total to pay",
      nightsWord: "nights",
      invalidDates: "Check-out date must be after check-in date.",
      genericError: "Something went wrong. Please try again.",
      networkError: "Could not reach the server. Please try again.",
      successTitle: "Booking confirmed!",
      successBody: "Thank you! Your booking has been recorded and paid successfully. We look forward to welcoming you at Casa31A.",
      cancelTitle: "Payment cancelled",
      cancelBody: "The booking was not completed. You can try again anytime."
    }
  },
  hu: {
    siteName: "Casa31A",
    tagline: "Panzió Borszéken",
    nav: { home: "Főoldal", rooms: "Szobák", gallery: "Galéria", contact: "Kapcsolat", book: "Foglalás" },
    home: {
      heroTitle: "Üdvözöljük a Casa31A-ban",
      heroSubtitle: "Otthonos panzió Borszék szívében, tökéletes pihenésre és hegyi túrázásra.",
      cta: "Elérhető szobák megtekintése",
      aboutTitle: "Rólunk",
      aboutBody: "A Casa31A egy családi panzió Borszéken, amely ásványvízforrásairól és tiszta hegyi levegőjéről ismert. Kényelmes szobákat, hagyományos reggelit és meleg, otthonias hangulatot kínálunk."
    },
    rooms: {
      title: "Szobáink",
      perNight: "/ éj",
      capacity: "fő",
      bookNow: "Szoba foglalása",
      loading: "Betöltés...",
      loadError: "Jelenleg nem sikerült betölteni a szobákat."
    },
    gallery: { title: "Fényképgaléria" },
    contact: {
      title: "Kapcsolat",
      address: "Borszék, Hargita megye, Románia",
      formName: "Név",
      formEmail: "Email",
      formMessage: "Üzenet",
      formSubmit: "Küldés"
    },
    booking: {
      title: "Foglalás",
      checkIn: "Érkezés dátuma",
      checkOut: "Távozás dátuma",
      guests: "Vendégek száma",
      fullName: "Teljes név",
      email: "Email",
      phone: "Telefon",
      submit: "Tovább a fizetéshez",
      totalLabel: "Fizetendő összeg",
      nightsWord: "éjszaka",
      invalidDates: "A távozás dátumának az érkezés után kell lennie.",
      genericError: "Hiba történt. Kérjük, próbálja újra.",
      networkError: "Nem sikerült kapcsolódni a szerverhez. Kérjük, próbálja újra.",
      successTitle: "Foglalás megerősítve!",
      successBody: "Köszönjük! A foglalását rögzítettük és sikeresen kifizette. Várjuk a Casa31A-ban.",
      cancelTitle: "Fizetés megszakítva",
      cancelBody: "A foglalás nem fejeződött be. Bármikor újra próbálkozhat."
    }
  }
} as const;

export function t(locale: Locale) {
  return translations[locale];
}

export function localeParams() {
  return locales.map((locale) => ({ params: { locale } }));
}
