CREATE TABLE `managers` (
  `id` integer PRIMARY KEY,
  `name` varchar(255),
  `email` varchar(255),
  `password` varchar(255)
);

CREATE TABLE `developers` (
  `id` integer PRIMARY KEY,
  `name` varchar(255),
  `email` varchar(255)
);

CREATE TABLE `project_developers` (
  `developer_id` integer,
  `project_id` integer
);

CREATE TABLE `projects` (
  `id` integer PRIMARY KEY,
  `name` varchar(255),
  `type_id` integer,
  `description` varchar(255)
);

CREATE TABLE `project_types` (
  `id` integer PRIMARY KEY,
  `name` varchar(255)
);

CREATE TABLE `tasks` (
  `id` integer PRIMARY KEY,
  `name` varchar(255),
  `description` varchar(255),
  `project_id` integer,
  `user_id` integer,
  `deadline` datetime
);

ALTER TABLE `projects` ADD FOREIGN KEY (`id`) REFERENCES `project_types` (`id`);

ALTER TABLE `project_developers` ADD FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`);

ALTER TABLE `project_developers` ADD FOREIGN KEY (`developer_id`) REFERENCES `developers` (`id`);

ALTER TABLE `tasks` ADD FOREIGN KEY (`project_id`) REFERENCES `projects` (`id`);

ALTER TABLE `tasks` ADD FOREIGN KEY (`user_id`) REFERENCES `managers` (`id`);
