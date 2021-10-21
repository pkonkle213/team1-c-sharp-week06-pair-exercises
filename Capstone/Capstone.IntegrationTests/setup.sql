-- Put steps here to set up your database in a default good state for testing
DELETE FROM reservation;
DELETE FROM space;
DELETE FROM category_venue;
DELETE FROM venue;


--Insert Fake Venue
SET IDENTITY_INSERT venue ON

INSERT INTO venue (id, name, city_id, description) VALUES (1, 'Bucktown', 1, 'Williams favorite place to eat'), (2, 'Easton Market Place', 2, 'Phils favorite place to waste time');

SET IDENTITY_INSERT venue OFF