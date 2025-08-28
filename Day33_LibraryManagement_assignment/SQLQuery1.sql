Create database LibraryDb_assignment

use LibraryDb_assignment

-- Insert Authors
INSERT INTO Authors (Name, Bio) 
VALUES 
('Stephen King', 'Master of horror and suspense'),
('J.R.R. Tolkien', 'Author of The Lord of the Rings'),
('Arthur C. Clarke', 'Science fiction writer and inventor');

-- Insert Genres
INSERT INTO Genres (Name) 
VALUES 
('Horror'),
('Epic Fantasy'),
('Space Opera'),
('Mystery');

-- Insert Books
INSERT INTO Books (Title, AuthorId) 
VALUES 
('The Shining', 1),
('The Fellowship of the Ring', 2),
('2001: A Space Odyssey', 3);

-- Link Books with Genres
INSERT INTO BookGenres (BooksBookId, GenresGenreId) 
VALUES 
(1, 1),  -- The Shining -> Horror
(1, 4),  -- The Shining -> Mystery
(2, 2),  -- The Fellowship of the Ring -> Epic Fantasy
(3, 3);  -- 2001: A Space Odyssey -> Space Opera