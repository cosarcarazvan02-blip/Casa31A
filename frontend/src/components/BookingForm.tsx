import { useMemo, useState } from "react";
import type { BookingStrings, Room } from "./types";

interface Props {
  room: Room;
  apiBaseUrl: string;
  strings: BookingStrings;
}

function todayIso() {
  return new Date().toISOString().slice(0, 10);
}

export default function BookingForm({ room, apiBaseUrl, strings }: Props) {
  const [checkIn, setCheckIn] = useState("");
  const [checkOut, setCheckOut] = useState("");
  const [guests, setGuests] = useState(1);
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [phone, setPhone] = useState("");
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const nights = useMemo(() => {
    if (!checkIn || !checkOut) return 0;
    const diff = (new Date(checkOut).getTime() - new Date(checkIn).getTime()) / (1000 * 60 * 60 * 24);
    return diff > 0 ? diff : 0;
  }, [checkIn, checkOut]);

  const total = nights * room.pricePerNightRon;

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError(null);

    if (nights <= 0) {
      setError(strings.invalidDates);
      return;
    }

    setSubmitting(true);
    try {
      const response = await fetch(`${apiBaseUrl}/api/bookings`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          roomId: room.id,
          checkIn,
          checkOut,
          numberOfGuests: guests,
          guestFullName: fullName,
          guestEmail: email,
          guestPhone: phone
        })
      });

      const data = await response.json();

      if (!response.ok) {
        setError(data.error ?? strings.genericError);
        return;
      }

      window.location.href = data.checkoutUrl;
    } catch {
      setError(strings.networkError);
    } finally {
      setSubmitting(false);
    }
  }

  return (
    <form className="booking-form" onSubmit={handleSubmit}>
      <div className="row">
        <label>
          {strings.checkIn}
          <input type="date" required min={todayIso()} value={checkIn} onChange={(e) => setCheckIn(e.target.value)} />
        </label>
        <label>
          {strings.checkOut}
          <input type="date" required min={checkIn || todayIso()} value={checkOut} onChange={(e) => setCheckOut(e.target.value)} />
        </label>
        <label>
          {strings.guests}
          <input
            type="number"
            required
            min={1}
            max={room.capacity}
            value={guests}
            onChange={(e) => setGuests(Number(e.target.value))}
          />
        </label>
      </div>

      <div className="row">
        <label>
          {strings.fullName}
          <input type="text" required value={fullName} onChange={(e) => setFullName(e.target.value)} />
        </label>
        <label>
          {strings.email}
          <input type="email" required value={email} onChange={(e) => setEmail(e.target.value)} />
        </label>
        <label>
          {strings.phone}
          <input type="tel" required value={phone} onChange={(e) => setPhone(e.target.value)} />
        </label>
      </div>

      {nights > 0 && (
        <p className="total">
          {strings.totalLabel}: <strong>{total} RON</strong> ({nights} {strings.nightsWord})
        </p>
      )}

      {error && <p className="error">{error}</p>}

      <button type="submit" disabled={submitting}>
        {submitting ? "..." : strings.submit}
      </button>

      <style>{`
        .booking-form { display: flex; flex-direction: column; gap: 1rem; margin-top: 1rem; }
        .row { display: flex; gap: 1rem; flex-wrap: wrap; }
        label { display: flex; flex-direction: column; font-size: 0.85rem; gap: 0.25rem; flex: 1; min-width: 10rem; }
        input {
          padding: 0.5rem;
          border: 1px solid rgba(0,0,0,0.2);
          border-radius: 4px;
          font-size: 1rem;
        }
        .total { font-size: 1rem; }
        .error { color: #b02a2a; }
        button {
          align-self: flex-start;
          background: #6a7f5e;
          color: white;
          border: none;
          padding: 0.75rem 1.5rem;
          border-radius: 6px;
          cursor: pointer;
          font-size: 1rem;
        }
        button:disabled { opacity: 0.6; cursor: not-allowed; }
      `}</style>
    </form>
  );
}
