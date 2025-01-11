drop table if exists wypo;
drop table if exists szef;
drop table if exists klienci;
drop table if exists ksiazki;

create table ksiazki (
    id_ksiazki int identity(1,1) primary key,
    tytul varchar(50) not null,
    autor varchar(50) not null,
    publikacja int not null,
    dostep bit not null default 1
);

create table klienci (
    id_klienta int identity(1,1) primary key,
    imie varchar(50) not null,
    nazwisko varchar(50) not null,
    email varchar(50) not null,
    haslo varchar(50) not null,
);

create table szef (
    id_szefa int identity(1,1) primary key,
    imie varchar(50) not null,
    nazwisko varchar(50) not null,
    email varchar(50) not null,
    haslo varchar(50) not null,
);

create table wypo (
    id_wypo int identity(1,1) primary key,
    id_ksiazki int not null,
    id_klienta int not null,
    data_wypo date not null default getdate(),
    data_oddanie date null,
    oddanie_klient bit not null default 0,
    constraint klucz_ksiazki foreign key (id_ksiazki) references ksiazki(id_ksiazki),
    constraint klucz_klienci foreign key (id_klienta) references klienci(id_klienta)
);


insert into ksiazki (tytul, autor, publikacja)
values 
('Czysty kod. Podrêcznik dobrego programisty', 'Martin Robert C.', 2014),
('Python od podstaw', 'Moska³a Marcin', 2022),
('Sztuczna inteligencja od podstaw', 'Feliks Kurp', 2023),
('Linux. Komendy i polecenia', 'Sosna £ukasz', 2022),
('Excel 365. Biblia', 'Alexander Michael Kusleika Dick', 2023);

insert into klienci (imie, nazwisko, email, haslo)
values 
('Ania', 'Nowak', 'ania.nowak123@np.pl', 'ania123'),
('Jan', 'Kowalski', 'jan.kowalski@np.pl', 'kowal98');

insert into szef (imie, nazwisko, email, haslo)
values 
('Magda', 'Jedrychowska', 'admin@np.pl', 'mocnehaslo'),
('Kacper', 'Kulik', 'admin2@np.pl', 'haslo');

insert into wypo (id_ksiazki, id_klienta, data_wypo)
values 
(1, 1, getdate());