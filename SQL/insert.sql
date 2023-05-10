use Game_Database_V2;
INSERT INTO GameModels(Title, ReleaseYear, Genres, Platforms, Price, Rating, AgeCategory, Edition)
VALUES
('Red Dead Redemption 2', 2018, 'Action, Adventure', 'PS4, Xbox One', 59.99, 9.0, 'PEGI18', 'Deluxe'),
('The Legend of Zelda: Breath of the Wild', 2017, 'Action, Adventure', 'Nintendo Switch', 59.99, 9.5, 'PEGI12', 'Premium'),
('Grand Theft Auto V', 2013, 'Action, Adventure', 'PS4, Xbox One', 29.99, 9.0, 'PEGI18', 'Normal'),
('The Witcher 3: Wild Hunt', 2015, 'Action, RPG', 'PS4, Xbox One, PC', 29.99, 9.5, 'PEGI18', 'Normal'),
('Super Mario Odyssey', 2017, 'Platformer', 'Nintendo Switch', 59.99, 9.3, 'PEGI3', 'Normal'),
('God of War', 2018, 'Action, Adventure', 'PS4', 39.99, 9.5, 'PEGI18', 'Deluxe'),
('Horizon Zero Dawn', 2017, 'Action, RPG', 'PS4', 19.99, 9.2, 'PEGI16', 'Normal'),
('Marvel''s Spider-Man', 2018, 'Action, Adventure', 'PS4', 39.99, 9.4, 'PEGI16', 'Premium'),
('Uncharted 4: A Thief''s End', 2016, 'Action, Adventure', 'PS4', 19.99, 9.0, 'PEGI16', 'Normal'),
('The Last of Us Part II', 2020, 'Action, Adventure', 'PS4', 59.99, 9.7, 'PEGI18', 'Deluxe'),
('Assassin''s Creed Odyssey', 2018, 'Action, RPG', 'PS4, Xbox One, PC', 29.99, 9.0, 'PEGI18', 'Normal'),
('Mario Kart 8 Deluxe', 2017, 'Racing', 'Nintendo Switch', 59.99, 9.0, 'PEGI3', 'Deluxe'),
('FIFA 21', 2020, 'Sports', 'PS4, Xbox One, PC', 59.99, 8.5, 'PEGI3', 'Normal'),
('Overwatch', 2016, 'FPS', 'PS4, Xbox One, PC', 19.99, 9.0, 'PEGI12', 'Normal'),
('Minecraft', 2011, 'Sandbox', 'PS4, Xbox One, PC', 19.99, 9.0, 'PEGI7', 'Normal'),
('Animal Crossing: New Horizons', 2020, 'Simulation', 'Nintendo Switch', 59.99, 9.5, 'PEGI3', 'Normal'),
('Call of Duty: Modern Warfare', 2019, 'FPS', 'PS4, Xbox One, PC', 59.99, 8.5, 'PEGI18', 'Normal');

INSERT INTO PublisherModels (name, rating)
VALUES
('Electronic Arts', 8.5),
('Ubisoft', 9.0),
('Activision Blizzard', 8.0),
('Nintendo', 9.5),
('Rockstar Games', 9.0),
('Sony Interactive Entertainment', 9.2),
('Microsoft Studios', 8.8),
('Valve Corporation', 9.5),
('CD Projekt', 9.7),
('Square Enix', 8.9),
('Bethesda Softworks', 8.3),
('Capcom', 9.1),
('Konami', 8.6),
('Sega', 8.7),
('Bandai Namco', 8.2),
('2K Games', 8.8),
('Gearbox Software', 8.0),
('Epic Games', 9.3),
('Paradox Interactive', 8.5);


INSERT INTO RegionModels (name, population, timezone)
VALUES
('North America', 579000000, 'UTC-10:00'),
('South America', 430000000, 'UTC-3:00'),
('Europe', 747000000, 'UTC+1:00'),
('Africa', 1216000000, 'UTC+2:00'),
('Asia', 4463000000, 'UTC+8:00'),
('Oceania', 42000000, 'UTC+10:00'),
('Antarctica', 1106, 'UTC+3:00'),
('Greenland', 56081, 'UTC-3:00'),
('Australia', 25600000, 'UTC+10:00'),
('Russia', 144000000, 'UTC+8:00');