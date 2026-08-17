export interface Room {
  id: string;
  name: string;
  description: string;
  capacity: number;
  pricePerNightRon: number;
  amenities: string[];
  imageUrls: string[];
}

export interface RoomsStrings {
  title: string;
  perNight: string;
  capacity: string;
  bookNow: string;
  loading: string;
  loadError: string;
}

export interface BookingStrings {
  title: string;
  checkIn: string;
  checkOut: string;
  guests: string;
  fullName: string;
  email: string;
  phone: string;
  submit: string;
  totalLabel: string;
  nightsWord: string;
  invalidDates: string;
  genericError: string;
  networkError: string;
}
