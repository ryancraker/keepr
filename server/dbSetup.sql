CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

ALTER TABLE accounts
ADD cover_img VARCHAR(255) COMMENT 'User Cover Image';

CREATE TABLE vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(255) NOT NULL,
  is_private BOOLEAN NOT NULL DEFAULT false,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id)
);

CREATE TABLE vault_keeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  keep_id INT NOT NULL,
  vault_id INT NOT NULL,
  creator_id VARCHAR(255) NOT NULL,
  Foreign Key (keep_id) REFERENCES keeps(id) ON DELETE CASCADE,
  Foreign Key (vault_id) REFERENCES vaults(id) ON DELETE CASCADE,
  Foreign Key (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
)

DROP TABLE vault_keep;

CREATE VIEW
keeps_with_kept AS
SELECT keeps.*, COUNT(vault_keeps.id)AS kept FROM keeps LEFT JOIN vault_keeps ON keeps.id = vault_keeps.keep_id GROUP BY keeps.id

SELECT * FROM keeps_with_kept

ALTER TABLE keeps
ADD COLUMN views INT ;