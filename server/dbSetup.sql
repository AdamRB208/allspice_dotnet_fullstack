CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

-- RECIPES SECTION STARTS HERE!!!!
CREATE TABLE recipes (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title VARCHAR(255),
    instructions VARCHAR(5000),
    img VARCHAR(1000),
    category ENUM(
        'breakfast',
        'lunch',
        'dinner',
        'snack',
        'desert',
        'mexican',
        'cheese',
        'italian',
        'soup',
        'coffee'
    ) NOT NULL,
    creator_id VARCHAR(255) NOT NULL,
    FOREIGN KEY (creator_id) REFERENCES accounts (id) ON DELETE CASCADE
);

SELECT * FROM recipes WHERE category = 'breakfast';

SELECT recipes.img, recipes.category FROM recipes;

SELECT recipes.*, accounts.*
FROM recipes
    INNER JOIN accounts ON accounts.id = recipes.creator_id
WHERE
    recipes.creator_id = @creator_id;

-- INGREDIENTS SECTION STARTS HERE!!!!

CREATE TABLE ingredients (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255),
    quantity VARCHAR(255),
    recipe_id INT NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipes (id) ON DELETE CASCADE
);

SELECT ingredients.*, recipes.*
FROM
    ingredients
    INNER JOIN recipes ON ingredients.recipe_id = recipe_id
WHERE
    ingredients.id = LAST_INSERT_ID();

DELETE FROM ingredients WHERE ingredients.id = @ingredientId;

-- FAVORITES SECTION STARTS HERE!!!!

CREATE TABLE favorites (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    recipe_id INT NOT NULL,
    account_id VARCHAR(255),
    FOREIGN KEY (recipe_id) REFERENCES recipes (id) ON DELETE CASCADE,
    FOREIGN KEY (account_id) REFERENCES accounts (id) ON DELETE CASCADE
);

INSERT INTO
    favorites (
        id,
        created_at,
        updated_at,
        account_id,
        recipe_id
    )
VALUES (
        @Id,
        @CreatedAt,
        @UpdatedAt,
        @AccountId,
        @RecipeId
    );

SELECT favorites.*, recipes.*
FROM favorites
    INNER JOIN recipes ON favorites.recipe_id = recipes.id
WHERE
    favorites.id = LAST_INSERT_ID();

SELECT * FROM favorites WHERE id = LAST_INSERT_ID();

SELECT favorites.*, accounts.*
FROM favorites
    INNER JOIN accounts ON favorites.account_id = accounts.id
WHERE
    favorites.id = @FavoritesId;

DELETE FROM favorites WHERE favorites.id = @FavoritesId;
