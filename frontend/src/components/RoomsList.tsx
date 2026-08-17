import { useEffect, useState } from "react";
import BookingForm from "./BookingForm";
import type { BookingStrings, Room, RoomsStrings } from "./types";

interface Props {
  apiBaseUrl: string;
  strings: RoomsStrings;
  bookingStrings: BookingStrings;
}

export default function RoomsList({ apiBaseUrl, strings, bookingStrings }: Props) {
  const [rooms, setRooms] = useState<Room[] | null>(null);
  const [error, setError] = useState(false);
  const [openRoomId, setOpenRoomId] = useState<string | null>(null);

  useEffect(() => {
    fetch(`${apiBaseUrl}/api/rooms`)
      .then((res) => {
        if (!res.ok) throw new Error("failed");
        return res.json();
      })
      .then(setRooms)
      .catch(() => setError(true));
  }, [apiBaseUrl]);

  if (error) return <p>{strings.loadError}</p>;
  if (!rooms) return <p>{strings.loading}</p>;

  return (
    <div className="rooms-grid">
      {rooms.map((room) => (
        <article key={room.id} className="room-card">
          <h3>{room.name}</h3>
          <p>{room.description}</p>
          <p className="meta">
            {room.capacity} {strings.capacity} · <strong>{room.pricePerNightRon} RON</strong> {strings.perNight}
          </p>
          {room.amenities.length > 0 && (
            <ul className="amenities">
              {room.amenities.map((a) => (
                <li key={a}>{a}</li>
              ))}
            </ul>
          )}
          <button onClick={() => setOpenRoomId(openRoomId === room.id ? null : room.id)}>
            {strings.bookNow}
          </button>
          {openRoomId === room.id && <BookingForm room={room} apiBaseUrl={apiBaseUrl} strings={bookingStrings} />}
        </article>
      ))}

      <style>{`
        .rooms-grid {
          display: grid;
          grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
          gap: 1.5rem;
        }
        .room-card {
          border: 1px solid rgba(0,0,0,0.1);
          border-radius: 8px;
          padding: 1.25rem;
          background: white;
        }
        .room-card h3 { margin-top: 0; }
        .meta { color: #5a5148; }
        .amenities { display: flex; flex-wrap: wrap; gap: 0.5rem; list-style: none; padding: 0; }
        .amenities li {
          background: #eef1ea;
          padding: 0.2rem 0.6rem;
          border-radius: 999px;
          font-size: 0.8rem;
        }
        button {
          background: #6a7f5e;
          color: white;
          border: none;
          padding: 0.6rem 1.2rem;
          border-radius: 6px;
          cursor: pointer;
        }
      `}</style>
    </div>
  );
}
