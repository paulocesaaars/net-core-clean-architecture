CREATE TABLE tb_user (
    id UUID PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    fullname VARCHAR(255) NOT NULL,

    CONSTRAINT email UNIQUE(email)
);

INSERT INTO tb_user(id, fullname, email) VALUES('af4bf17c-fca7-453d-a9eb-251f3837da10', 'Paulo César de Souza', 'paulocesaaars@gmail.com');
INSERT INTO tb_user(id, fullname, email) VALUES('883cf9fa-f7e9-43d4-a8cb-51fd65863ba6', 'Bruna Stefano Marques', 'bruna@gmail.com');