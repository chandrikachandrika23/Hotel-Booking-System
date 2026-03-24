export interface Booking {
  bookingId: number;
  userId: number;
  roomId: number;

  checkInDate: Date;
  checkOutDate: Date;

  totalAmount: number;
  status: string;

  // 🔹 Navigation properties (from backend Include)
  room?: Room;
}

export interface Room {
  roomId: number;
  roomNumber: string;
  price: number;

  hotel?: Hotel;
}

export interface Hotel {
  hotelId: number;
  name: string;
  location: string;
}
