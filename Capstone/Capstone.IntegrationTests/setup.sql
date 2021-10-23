-- Put steps here to set up your database in a default good state for testing
DELETE FROM reservation;
DELETE FROM space;
DELETE FROM category_venue;
DELETE FROM venue;
DELETE FROM category;
DELETE FROM city;
DELETE FROM state;

--Fake State
INSERT INTO state (abbreviation, name) VALUES ('CA', 'Boo')

--Fake City
SET IDENTITY_INSERT city ON

INSERT INTO city (id, name, state_abbreviation) VALUES (1, 'Los Angeles', 'CA')

SET IDENTITY_INSERT city OFF

--Fake Category
SET IDENTITY_INSERT category ON

INSERT INTO category (id, name) VALUES (1, 'OOchie')

SET IDENTITY_INSERT category OFF

--Insert Fake Venue
SET IDENTITY_INSERT venue ON

INSERT INTO venue (id, name, city_id, description) VALUES (1, 'Bucktown', 1, 'Williams favorite place to eat'), (2, 'Easton Market Place', 1, 'Phils favorite place to waste time');

SET IDENTITY_INSERT venue OFF

--Fake Category Venue
INSERT INTO category_venue (venue_id, category_id) VALUES (1, 1)

--Fake Spaces
SET IDENTITY_INSERT space ON

INSERT INTO space (id, venue_id, name, is_accessible, open_from, open_to, daily_rate, max_occupancy) VALUES (1, 1, 'Mississippi', 1, NULL, NULL, 5000, 1000), (2, 1, 'Fubar', 0, 1, 8, 42, 13);

SET IDENTITY_INSERT space OFF

--Fake Reservation
SET IDENTITY_INSERT reservation ON

INSERT INTO reservation (reservation_id, space_id, number_of_attendees, start_date, end_date, reserved_for) VALUES (1, 1, 1000, '2021/01/01', '2021/09/01', 'Happy Gilmore')

SET IDENTITY_INSERT reservation OFF