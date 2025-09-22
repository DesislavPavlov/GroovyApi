-- phpMyAdmin SQL Dump
-- version 4.7.1
-- https://www.phpmyadmin.net/
--
-- Host: sql7.freesqldatabase.com
-- Generation Time: Jun 25, 2025 at 12:56 PM
-- Server version: 5.5.62-0ubuntu0.14.04.1
-- PHP Version: 7.0.33-0ubuntu0.16.04.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sql7761052`
--

-- --------------------------------------------------------

--
-- Table structure for table `artist`
--

CREATE TABLE `artist` (
  `artist_id` int(11) NOT NULL,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `image_url` varchar(255) DEFAULT NULL,
  `color` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `artist`
--

INSERT INTO `artist` (`artist_id`, `name`, `image_url`, `color`) VALUES
(1, 'The Weeknd', 'https://localhost:7021/uploads/063d63bde40b4d90adfdd6f46ca171804af54c4d12a9e1d5d8877fb88a6ed212.webp', '#800080'),
(2, 'Dua Lipa', 'https://localhost:7021/uploads/2578ef3660ac8d5fb387be859a094211a1107c01cce1c7ca15091726ca4d9f49.webp', '#ff10f0'),
(3, 'Ariana Grande', 'https://localhost:7021/uploads/f4fc256cb8b992c992fd7dc932912f8c4e3448c7cbb407b1ca2e26e479593576.webp', '#e6e6fa'),
(4, 'Harry Styles', 'https://localhost:7021/uploads/7169ef8666861245b7ee88126d233d9a2d2119b24f81742b4672a41ea6d4eb98.webp', '#ffa500'),
(5, 'Billie Eilish', 'https://localhost:7021/uploads/6c2bd9b8f153f0277f05c406f9c5b443505340d65b8928f5b6d640601e8e4c20.webp', '#39ff14'),
(6, 'Niall Horan', 'https://localhost:7021/uploads/ba9f4467500a40ab8c39bd7cdda0377ce89fcc3065c77c48308bb72d2f06ea79.webp', '#d1e5b4'),
(7, 'Bruno Mars', 'https://localhost:7021/uploads/1531e3525015a6cf013cf36f84a6a7e8e7b85f6bcaad4d55370ea8f3ad4ad114.webp', '#ffd700'),
(8, 'Katy Perry', 'https://localhost:7021/uploads/c5e32e9b4d7a3ddc760f6313f8bbab3a531cb40dff7fd6eaa8789ef6154de5d2.webp', '#7df9ff'),
(9, 'Shawn Mendes', 'https://localhost:7021/uploads/bad4827a35f2d372cc1767d244585be1a95c76743083b3d159c2b7beb944e1f1.webp', '#228b22'),
(12, 'Zayn Malik', 'https://localhost:7021/uploads/b936cb0133da8b5ed82622c0991c68cbd8cc573895d0afe4f1739f6cd1c6d27b.webp', '#ff0000'),
(13, 'Led Zeppelin', 'https://localhost:7021/uploads/72c07b91354dcce1d63c3edd95d010111a667889aac83446b9f65166b8a7a707.webp', '#ffd700'),
(14, 'AC/DC', 'https://localhost:7021/uploads/324ccbf55c9cfb04b8d8b8f33ca2da9a901211f12637ba76908366bef1c46456.webp', '#ff0000'),
(15, 'Nirvana', 'https://localhost:7021/uploads/98f0c5484aedcb045dc32505eb470bb9c3bf7a9ff2a8e069a91a3f8a0952bf54.webp', '#00008b'),
(16, 'Alice in Chains', 'https://localhost:7021/uploads/414dc014b0a63d2857ce849dd55b3ba81eabf742148b671d95951e2c781d2118.webp', '#006400'),
(17, 'Aerosmith', 'https://localhost:7021/uploads/f11716f91d0fda75a48b879b8485eb9033483e76bfffb112abac9d8682f8f2b8.webp', '#c0c0c0'),
(18, 'Metallica', 'https://localhost:7021/uploads/c3cac1ebafdf482f3c8bd4ee391c88d538e1c0101b70f4e5c54fc8b956c02446.webp', '#000000'),
(19, 'Megadeth', 'https://localhost:7021/uploads/791fc783922c3b965b1a8e1e6a55892ebec6f554e196b45f2dffd04a63418245.webp', '#00008b'),
(20, 'Rammstein', 'https://localhost:7021/uploads/a53bdf1bf466a4e053458b3b0b5c7d767793812b037f7868f8a32a8d9375973b.webp', '#ff4500'),
(21, 'Korn', 'https://localhost:7021/uploads/b6698697a7a0ed97520e5f2cca0dc051634ef26176e853c98026a8f996c44bb9.webp', '#000000'),
(22, 'Slipknot', 'https://localhost:7021/uploads/c20c43a92755f5f574b92ade11cd8a31bc4cb5e1beb9c0c414c4f6e53a5aeff2.webp', '#8b0000'),
(23, 'H.I.M', 'https://localhost:7021/uploads/a389835d23b04c35fe582c2d9a48b03db3039e6a2ce67a3df4ef9123d7e12791.webp', '#ffc0cb'),
(24, 'Kendrick Lamar', 'https://localhost:7021/uploads/889b7eeab3441c367b41ab527fa8d79ead86ff8d42666856bcb543b029e7a2ad.webp', '#808080'),
(25, 'J. Cole', 'https://localhost:7021/uploads/671a1b44b5e83be68bf03dd2825e6e1dfa4c5b678a37a5298ae0265e504961cf.webp', '#228b22'),
(26, 'Drake', 'https://localhost:7021/uploads/0ed5a77796e2699bbc28615d88adfd5a8f7e83b589eea4cbf749bc03dc0f6b86.webp', '#ffc0cb'),
(27, 'Nas', 'https://localhost:7021/uploads/e816e1b1de76985a00cca0a922bf5dc56cff9155a0b953dc43ed692a56cf2065.webp', '#000000'),
(28, 'Lil Wayne', 'https://localhost:7021/uploads/a8ea59cda7af7206c698d8eb0099658fe31a6db42f4aa9fdc8551334ea490e3b.webp', '#ffff00'),
(29, 'Travis Scott', 'https://localhost:7021/uploads/db76d0d4b6a613237f7d8e3752beda5315c093277e49e1e2f71464293e751860.webp', '#800080'),
(30, 'Jay-Z', 'https://localhost:7021/uploads/bd60697cc3df58dd2a02f339c94d73b0e6e93c83b4c1a6b23da62c88b4da8905.webp', '#00008b'),
(31, 'Future', 'https://localhost:7021/uploads/66039ec40fd3809fb71b1930c03e826d0286dc4d140187cfba1e4585e8729752.webp', '#7df9ff'),
(32, 'Tyler, The Creator', 'https://localhost:7021/uploads/79171e6edf95e4ab28f206a41b3d7af4d9093b2f166265f6ad825ff507ead2fd.webp', '#ffff00'),
(33, 'Eminem', 'https://localhost:7021/uploads/fc35ab5dcf4fcdd924c5308025843a12b65a270847664759ce5c6fa72fb83359.webp', '#ffd700'),
(34, 'Kanye West', 'https://localhost:7021/uploads/780f349852b12164d25b8714db4b2e37514e1bc96de36d3f731ec9400a159ad2.webp', '#a52a2a'),
(35, 'Avicii', 'https://localhost:7021/uploads/641d6c0d8ab8bcb5a1e163572035c0f4d5dc1b2052d77b6b72d86f76cd78997e.webp', '#87ceeb'),
(36, 'Calvin Harris', 'https://localhost:7021/uploads/04f8d5ccf283affb7b74d7feb1debae839ac789bb21ae481cadc924f81de3be9.webp', '#ffff00'),
(37, 'Marshmello', 'https://localhost:7021/uploads/ef590e9c02735e637dc5dfbdd4c14808cd9542fbbca66105f4adfac6f01d5be4.webp', '#ffffff'),
(38, 'Zedd', 'https://localhost:7021/uploads/0f643e34ce6909748a0dd18795475c2c66552b2b2ed229043e8ed62d24730b6c.webp', '#800080'),
(39, 'Skrillex', 'https://localhost:7021/uploads/1c40af667ee426147a30fefc3ac7f07934da4883cbcce054d33285d714b98e8e.webp', '#ff10f0'),
(40, 'Deadmau5', 'https://localhost:7021/uploads/5e6df1c963047532192edb35352115462dc1b934980e28af8882d1bd3ab6c711.webp', '#000000'),
(41, 'The Chainsmokers', 'https://localhost:7021/uploads/45699b7bf508df5f623a5ce75b6ae661bccbf50a32223165a3dcaaa19faa6b07.webp', '#ff7f50'),
(42, 'Steve Aoki', 'https://localhost:7021/uploads/790d06650e083e4e2be8fbc233cad1750fe92d227d8aa2ae6dd675be97d5d463.webp', '#00ffff'),
(43, 'Hardwell', 'https://localhost:7021/uploads/bfd3101d700f118f2d5a92b7f07ecc5e3ac650ab8054468dbfe3ec33e398d5a9.webp', '#ffa500'),
(44, 'Dimitri Vegas & Like Mike', 'https://localhost:7021/uploads/782ff1fdc8cd5f63869cbb519c4674a81f3712a313c271cc411d29e9c2a02123.webp', '#ff0000'),
(46, 'Aretha Franklin', 'https://localhost:7021/uploads/5b9303ec0326318cb6853f0823dfebdf4b5a28aa1df21011361b959a84a9d2e3.webp', '#ffd700'),
(47, 'Marvin Gaye', 'https://localhost:7021/uploads/fc7e71fd652ee8330ec5df9b9e256ebe80ac755b0cf2b3ca8e19f9067403152a.webp', '#800080'),
(48, 'Beyonce', 'https://localhost:7021/uploads/23b8721de1219b31ab0ec14e9923672fc3e6c0f338015e876c4d38ac7d415737.webp', '#50c878'),
(49, 'Usher', 'https://localhost:7021/uploads/9c95b321e147ddd696fb694953b7701442ee4f1f6f11201ef49c12e736b39ee2.webp', '#0000ff'),
(50, 'Frank Ocean', 'https://localhost:7021/uploads/d470f54abf81cfb8dd3f7bd17e5271b6f681b8ad371cb0997ced5b56974ff65d.webp', '#e6e6fa'),
(51, 'SZA', 'https://localhost:7021/uploads/3b2fd16e092680e1f7b38c4c09a5dd93e582b7e85ed3aa39a89349a23c5a3410.webp', '#ffa500'),
(52, 'Alicia Keys', 'https://localhost:7021/uploads/0ccd0ed882c7f4b2f1796f43d968b8e566445e41092be1a40ce92b4bcb0aa0a9.webp', '#fffff0'),
(53, 'John Legend', 'https://localhost:7021/uploads/8f2ee8d00a72f04c029799ddebe3e3e8a924ac413e0e4fd26d8accc12a8ba229.webp', '#ff0000'),
(54, 'DAngelo', 'https://localhost:7021/uploads/ff4922012248bcae44821a5c2296e00cc242f1c5fc7a340523b830e978c65066.webp', '#a52a2a'),
(55, 'Rihanna', 'https://localhost:7021/uploads/c94a325fa9d15947032a151a155624180aed1327ffc2c36a10bbd10928b7a363.webp', '#ff2400'),
(56, 'Jodeci', 'https://localhost:7021/uploads/c0a3c592ee0c1e1e4dd1784311837b28bbc9ecdee0cac77f02d0a1520f33d60b.webp', '#ff0000'),
(57, 'Azis', 'https://localhost:7021/uploads/0b855677251eeac2795fc175383c8e7438f4882539cb7ce1da5977e6315250bf.webp', '#ffd700'),
(58, 'Galena', 'https://localhost:7021/uploads/0c30918d5bfe8883792840132622886afda9153db2c53f76af4f005cba358b23.webp', '#c0c0c0'),
(59, 'Konstantin', 'https://localhost:7021/uploads/7f0d7d7b9b7646bc701f6bd37ff9deb7b47ca465b54e64ea25c1f4cb07905ced.webp', '#d2691e'),
(60, 'Emilia', 'https://localhost:7021/uploads/7983335f3deb33787678504881309b65a903494a46a38fa0635396b247ad6cb8.webp', '#0000ff'),
(62, 'Sofi Marinova', 'https://localhost:7021/uploads/8cf9fb1dba448f007121aef3e9f9b7745b3192dffa27cbd05cda836a58c1ed1b.webp', '#ffd700'),
(63, 'Fiki', 'https://localhost:7021/uploads/4e29c375d906492e78ca17e05fe3d92a75bcda6f60edcd50f90fe0d2b33fff05.webp', '#ffffff'),
(64, 'Toni Storaro', 'https://localhost:7021/uploads/49749a3747100a7a6ba99f186851ebb5f6696a5c08740944c4eca75e0d592727.webp', '#406196'),
(65, 'Medi', 'https://localhost:7021/uploads/91650a323aec7b4fcd15c3e686e5bdf536f8ac57ab9e34d1799e488aa843ebc5.webp', '#000000'),
(67, 'Andrea', 'https://localhost:7021/uploads/e9d14766cb534955c639767f608b83c39d299362da55f490d4ce5a7edcce47c3.webp', '#faf0be'),
(71, 'Slavi Trifonov', 'https://localhost:7021/uploads/991f40d2d6cb6c3080724998fccb0c8ec29bd0f11c7458dcffac5f64831d120e.webp', '#0d0d0d'),
(85, 'Porter Robinson', 'https://localhost:7021/uploads/2b7b6af4dab5660d8dc2a284dccfa0c7b41a0b23e08f2727d828ab661735539b.webp', '#e6e6fa'),
(119, 'Ustata', 'https://localhost:7021/uploads/dcdfa12902e226f1f82c81183fdb4ed10fb21eaaef64e2d6d217e8fa741413ba.webp', '#a1d0ed'),
(120, 'Post Malone', 'https://localhost:7021/uploads/d71c95ecd69a410328388f26824192262b8a5f7f115af06e010d9619adb38574.webp', '#7e85a0'),
(121, 'Иво Най-доброто', 'https://localhost:7021/uploads/28a438fe2ea502b6098303fb358c8dc731912ded251a8fc4641fa234a2ae2bcd.webp', '#b36565');

-- --------------------------------------------------------

--
-- Table structure for table `artist_genre`
--

CREATE TABLE `artist_genre` (
  `id` int(11) NOT NULL,
  `artist_id` int(11) NOT NULL,
  `genre_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `artist_genre`
--

INSERT INTO `artist_genre` (`id`, `artist_id`, `genre_id`) VALUES
(170, 17, 2),
(171, 57, 6),
(172, 16, 2),
(173, 60, 6),
(176, 59, 6),
(179, 63, 6),
(180, 65, 6),
(181, 62, 6),
(182, 71, 6),
(183, 119, 6),
(184, 14, 2),
(185, 52, 5),
(186, 67, 6),
(187, 46, 5),
(188, 3, 1),
(189, 35, 4),
(190, 54, 5),
(191, 48, 5),
(192, 5, 1),
(193, 7, 1),
(194, 36, 4),
(195, 40, 4),
(196, 44, 4),
(197, 26, 3),
(198, 2, 1),
(199, 33, 3),
(200, 50, 5),
(201, 31, 3),
(202, 58, 6),
(203, 23, 2),
(204, 43, 4),
(205, 4, 1),
(206, 25, 3),
(207, 30, 3),
(208, 56, 5),
(209, 53, 5),
(210, 34, 3),
(211, 8, 1),
(212, 24, 3),
(213, 21, 2),
(214, 13, 2),
(215, 38, 4),
(216, 12, 1),
(217, 49, 5),
(218, 32, 3),
(219, 29, 3),
(221, 1, 1),
(222, 41, 4),
(223, 51, 5),
(224, 42, 4),
(225, 22, 2),
(226, 39, 4),
(227, 9, 1),
(228, 55, 5),
(229, 20, 2),
(232, 15, 2),
(234, 6, 1),
(235, 47, 5),
(236, 27, 3),
(237, 18, 2),
(238, 19, 2),
(239, 37, 4),
(240, 28, 3),
(241, 64, 6),
(242, 120, 3),
(244, 85, 4),
(245, 121, 29);

-- --------------------------------------------------------

--
-- Table structure for table `favourite`
--

CREATE TABLE `favourite` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `song_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `favourite`
--

INSERT INTO `favourite` (`id`, `user_id`, `song_id`) VALUES
(28, 30, 49),
(29, 30, 51),
(30, 30, 36),
(35, 30, 61),
(48, 48, 38),
(49, 48, 53),
(50, 48, 62),
(54, 48, 138),
(55, 30, 63),
(56, 30, 66),
(57, 30, 38);

-- --------------------------------------------------------

--
-- Table structure for table `genre`
--

CREATE TABLE `genre` (
  `genre_id` int(11) NOT NULL,
  `name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `color` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `genre`
--

INSERT INTO `genre` (`genre_id`, `name`, `color`) VALUES
(1, 'Поп', '#FFC0CB'),
(2, 'Рок / Метал', '#000000'),
(3, 'Рап', '#FFD700'),
(4, 'EDM', '#0000ff'),
(5, 'R&B', '#800080'),
(6, 'Поп-Фолк (Чалга)', '#FF0000'),
(29, 'ИвоЖанр', '#cdfb28');

-- --------------------------------------------------------

--
-- Table structure for table `song`
--

CREATE TABLE `song` (
  `song_id` int(11) NOT NULL,
  `title` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `song_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `cover_url` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `color` varchar(10) NOT NULL,
  `clicks` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `song`
--

INSERT INTO `song` (`song_id`, `title`, `song_url`, `cover_url`, `color`, `clicks`) VALUES
(36, 'Точно сега - Азис и Устата', 'https://localhost:7021/uploads/bbf4bd64a05462eb50d861a46b4e7b93aae2de9249389d74edd0ec872f1ac431.webm', 'https://localhost:7021/uploads/a5723be4039e2afd9933a3db17b21c7cecbc67f6c39d953d4a1079934d069b6d.webp', '#dd4646', 74),
(38, 'Ледена кралица - Азис', 'https://localhost:7021/uploads/9605ab6f552aa2b37638a3b4dcf1c7cede23b74bdf76e2642fc248350845042b.webm', 'https://localhost:7021/uploads/79305af6cc44808d3891958f3ad45dd78e81b30d362baa583d29b8a7e0162ba3.webp', '#92cadd', 14),
(39, 'Черните очи - Азис и Малина', 'https://localhost:7021/uploads/e0a8bd373705684be042303f680356c80de868225211b16df9e9a6ad55ead1dc.webm', 'https://localhost:7021/uploads/3dde0fc8c3ff826b46f7c02359c098b1e3b05d7d85bad76f453f1fa48f2d7dd0.webp', '#d6c2c2', 22),
(40, 'Автомонтьор - Азис', 'https://localhost:7021/uploads/d70264b5ef6733d546184e06c9f5df063becde5a33e966efad8b4e779b0a52a1.webm', 'https://localhost:7021/uploads/e14a102b6d13f84a7b36e5b0a0196d5abf9b39c69f1b943d0a06635ef5e25b33.webp', '#e0cd85', 14),
(41, 'По, по, по - Азис и Галена', 'https://localhost:7021/uploads/477f4c64797da5b6f1b3c36d23b3bf5189f60a0dba8a0bb5fb0940b81df9586f.webm', 'https://localhost:7021/uploads/4f6dbebf66d9b2c8511495bb53550abf327953be1ac584225d9e80d6288ad2d7.webp', '#e3e3e3', 5),
(42, 'Не сме безгрешни - Азис и Глория', 'https://localhost:7021/uploads/26a8be6ad5e4977f3d8fd0d563914193754fe9de4f89ebada5b7b2b24ed50fc3.webm', 'https://localhost:7021/uploads/fa7364acf9415e0bcdfa3a96a36bd22b2c77a5e55f8521dd4b5ab8ec75e3fa3a.webp', '#9dbee2', 5),
(43, 'Мръсница - Азис', 'https://localhost:7021/uploads/e71ecba62fcce22708bf89fc1e74e4644cf900f316bf15d22805bc95b0817d58.webm', 'https://localhost:7021/uploads/d871e3595dace44266fe96f0f4cb96a94b3878b54abc97a3937d8e04d22c10dc.webp', '#b84c4c', 3),
(49, '100 SMS-a - Слави Трифонов', 'https://localhost:7021/uploads/ea750ece1c320a1ed776f2e9d56a61e7621715d29fc3370cb36c72d8e0e40235.webm', 'https://localhost:7021/uploads/3145d35652c6a6faf2c7e0347ea228da2de0fe0cd6f3c539f34a9d1ca5aee1ec.webp', '#f0edc1', 6),
(50, 'Френската гимназия - Слави Трифонов', 'https://localhost:7021/uploads/99c8d14559c72c45ea9fc09a09c115b0ec4f472fa5cec2596876c4ab1282d1b0.webm', 'https://localhost:7021/uploads/4939398a8f7d8ec6a90c93e7bb0678841dfca8466936dd5490e320f0024f32da.webp', '#dc9ae4', 9),
(51, 'Пружината - Слави Трифонов', 'https://localhost:7021/uploads/3ad17350b4ef98205141c2325a1941429370e2105a01117aca6737a75b15300f.webm', 'https://localhost:7021/uploads/b9d7ba550be970b0b1b01d4e5e5c9425ff1b3172350c15c7f7480625fa4ccd78.webp', '#949494', 54),
(52, 'Студио Х - Слави Трифонов', 'https://localhost:7021/uploads/3f5cbc54962d50653d81d4bc35c61d1c0f62e950318a13eaedd7b557e41f605d.webm', 'https://localhost:7021/uploads/11817a09372d46518fee45968e0400fa2440223d19ea4addbed855220b0587a1.webp', '#2e2e2e', 5),
(53, 'Нирвана кючек - Слави Трифонов', 'https://localhost:7021/uploads/05e1a0336aca366b9ddfbee905c6e9785e577113e7e8d0d7147cff728a56e5c2.webm', 'https://localhost:7021/uploads/4acd1233a7e38d3dd5813fe7496c33251c1c9af4b24d0cac22ba6407cad752be.webp', '#6fa0b8', 4),
(54, 'Cryin\' - Aerosmith', 'https://localhost:7021/uploads/121226bd75ad84ff04a4cd35ea77bf37318df38123cf77a4ee8b06bd9069cf30.webm', 'https://localhost:7021/uploads/0f219d61d61adced860738bc5d261c78617bae5e8bceb4b951be3c68abedb075.webp', '#e2c983', 3),
(55, 'Circles - Post Malone', 'https://localhost:7021/uploads/f20b5c9f53d8491a28f74784b453acf6247325e25255bb4baea1c530c1f01d42.webm', 'https://localhost:7021/uploads/e80474ba11ee1d92138dc19cbe59edb6a70af32e15ae6387a3edb162bd277b65.webp', '#1d594c', 1),
(57, 'Du Hast - Rammstein', 'https://localhost:7021/uploads/d74470b14e2edfa66f1b01d5cad1e3a2dda13e7119fcf557495d009465dc3b8b.webm', 'https://localhost:7021/uploads/e3e422e569c335b5fda7873b1318511f375533b3f7092c49609f543e824c6824.webp', '#8da2a5', 16),
(58, 'FE!N - Travis Scott', 'https://localhost:7021/uploads/ff93bb2f4757f76c7851a17a97e9a2eb56699476dd7fc6d78d909ed11acb505e.webm', 'https://localhost:7021/uploads/9241bf03c5e7edb9138231b05ed4521e0bc0ca164c2f03d77e4b7e73f6160a6d.webp', '#577a70', 3),
(59, 'It Will Rain - Bruno Mars', 'https://localhost:7021/uploads/b4283c74bb75513c7f96f35059e31511af094354993d8793c4fe745c6973f754.webm', 'https://localhost:7021/uploads/312018031b0921f9c6cf170613fd9c6e01d12193528edcd8e57b0b8312702507.webp', '#f3c677', 10),
(60, 'Du Riechst So Gut - Rammstein', 'https://localhost:7021/uploads/05eb2e2f438b787087ce7ca947251c70972a4607e94b33dab5e794dc1226046b.webm', 'https://localhost:7021/uploads/f64c97bd092c6794a6d8db04fa39573cbeb01d194ab42fe4ef098b33d42ff087.webp', '#e9e6d2', 7),
(61, 'Feuer Frei - Rammstein', 'https://localhost:7021/uploads/98aa0b340b69bbe736d4c5656f1f7641267004815d790d5ffc8829efee97f796.webm', 'https://localhost:7021/uploads/c45f94749d1e333ff12c2f61a3964e436bad31ba9e529afc5a0b87a7a2eb244e.webp', '#da8c44', 1),
(62, 'Immigrant Song - Led Zeppelin', 'https://localhost:7021/uploads/803a3d0c0753fc73720f5187ec716fa69a58658ea0f547ccea6f46ec8b8e91d3.webm', 'https://localhost:7021/uploads/2c55521ddff0013e3d1bc6a36818d16c83dbdf261aed23e731dce341b3c9b395.webp', '#e6dba8', 2),
(63, 'Stairway to Heaven - Led Zeppelin', 'https://localhost:7021/uploads/76810dca685bda87c4e929aee9692e3c06ab56b5d9e2a753671e3fe8a64d7860.webm', 'https://localhost:7021/uploads/418cb7e76238ca3a3a6209c96cdac92e7ad2ee69279db6972f42447c489ed0b3.webp', '#dbde4f', 3),
(64, 'Back in Black - AC/DC', 'https://localhost:7021/uploads/7c76ff00a119377b39e60f49774f66ec33ec8b8c8e1b7d0be27397d03952eebe.webm', 'https://localhost:7021/uploads/fb0ba166c234e07f7b87bafd143eb2759a535c809e70651e998c37c57ee365b6.webp', '#fbfad5', 3),
(65, 'Highway to Hell - AC/DC', 'https://localhost:7021/uploads/6d529ad989095c118f1dd7a6af27043eb61a0baf268559365db7e9b2f909fdb2.webm', 'https://localhost:7021/uploads/7067a1f4573d9f21e47cfdec6e93d7b773c41143c1277700a5c74f9ac187b134.webp', '#a83200', 1),
(66, 'Thunderstruck - AC/DC', 'https://localhost:7021/uploads/3888a221f520362ed2679ef19ca420086828981957db28d9e2fdf8427f61546e.webm', 'https://localhost:7021/uploads/fbc9d3d1daecb4e28a89861e6777caee1fe3579cf0c4aaff63952f2c1f7327a1.webp', '#df4e4e', 2),
(67, 'Angel - Aerosmith', 'https://localhost:7021/uploads/663360afa2fa0ad33a2eb0adf064eba8cc1bc7eecc83c1888da1bc1521e070a6.webm', 'https://localhost:7021/uploads/6593a744a630f26cf30de2e7a49b868f6e9fb3867bd5d8854d54b0092b703873.webp', '#f0f4cd', 3),
(68, 'SICKO MODE - Travis Scott & Drake', 'https://localhost:7021/uploads/6f3735720eaf2c8f76e7b4ee3de57c6adb231fa2ba75ae48c75105ee52ef96bf.webm', 'https://localhost:7021/uploads/2f86d0af2c9badd49488a28a04215f7dcfbb7f2ea37945151b2737e67b024bc7.webp', '#ada114', 2),
(69, '4x4 - Travis Scott', 'https://localhost:7021/uploads/8198c415b7c52bec5cd3599ea752e1c8e0155a589aa8d8d067e9bfc7669ccea0.webm', 'https://localhost:7021/uploads/05e63b28ae337a260933935c6bb5a77dfbe5655d47898f7b0350d092b4d26deb.webp', '#ddcfc5', 0),
(70, 'White Version - Post Malone', 'https://localhost:7021/uploads/a527a972207db05b85e8d42c657c7c5498f27ec7bf3c40101d0664369fc6b6d3.webm', 'https://localhost:7021/uploads/29a6da4036f56c8a56bd9f3015b321c2a920fe5aa83c9c977595eb308499a907.webp', '#bdbdbd', 1),
(71, 'Better Now - Post Malone', 'https://localhost:7021/uploads/fca58d8022812f2be887b732d544e13dbb58c37e02a8b46aa65e657b038adac8.webm', 'https://localhost:7021/uploads/5d74b5b815bdd75c34758069e3102fc9f4b1b09505ec807b393d5d667971e90b.webp', '#d3d600', 1),
(72, 'Jesus Walk - Kanye West', 'https://localhost:7021/uploads/52f3cba731dc4300371b5e447a401a8cde711d4702f63bb07d2244ac0c7d8803.webm', 'https://localhost:7021/uploads/1253fe6db5a77ed4b95071acc1ac1ea3f696e03c723607e5883cc85ce74736d3.webp', '#dbb5de', 2),
(73, 'POWER - Kanye West', 'https://localhost:7021/uploads/91fb13ff5fa6ee95269f858b10938a1f92a97fe502a2c3cf3e6dc4ccdb089336.webm', 'https://localhost:7021/uploads/96ad0118cc5cc1d06f037959a85ab4f6cb5635850d1490e9a95fffa67bd4d036.webp', '#797fcd', 1),
(74, 'Rosenrot - Rammstein', 'https://localhost:7021/uploads/fa78f597186ea5d1c3638b87987179311f650a9e71a2b08811ee1a3e6d9630a6.webm', 'https://localhost:7021/uploads/60642659405a9915aa61f9198ecd0ce5ead5db081b19fe0c6c71cd8b186fed06.webp', '#578dc7', 4),
(75, 'Amerika - Rammstein', 'https://localhost:7021/uploads/c58276064b0e6cdc0b557984b9ab5ad606a34ab5feded99d03c55ae4a2c532cb.webm', 'https://localhost:7021/uploads/e8620a4063b54b7665fcd9679d387e25452bd176bbb8dfc92b570df7844a26dc.webp', '#7382f2', 1),
(76, 'Deutschland - Rammstein', 'https://localhost:7021/uploads/fca6a677714b5e9b41758d744c0829932e42751caf6e7cda70af2f6eb0e71c94.webm', 'https://localhost:7021/uploads/a6a009f41f2fab9b8928aa2622bc6345e11e864bca353397ff658680ff001e64.webp', '#d4d4d4', 1),
(77, 'Sonne - Rammstein', 'https://localhost:7021/uploads/e5c22edf068775f4106c995769ce951f581b74a87744ee1beb4eb083b61a6da5.webm', 'https://localhost:7021/uploads/d13682357ba814704ad9d909e927737ff0a9829fabc4bf87a13668dfe0387786.webp', '#eedad8', 1),
(78, 'Fade To Black - Metallica', 'https://localhost:7021/uploads/4100ad90040a1f79de30c0dcfde9b4d9ddfd7ff09a3a26503e20e8f17b183058.webm', 'https://localhost:7021/uploads/d775f1fd01a5f443c672653577b6a45575eb006898c223316939943e228a0bda.webp', '#3f3f99', 0),
(79, 'For Whom The Bell Tolls - Metallica', 'https://localhost:7021/uploads/93f09d707f981887c22820d1454d67ee1529900e52a6265ebad9560c4640c725.webm', 'https://localhost:7021/uploads/d775f1fd01a5f443c672653577b6a45575eb006898c223316939943e228a0bda.webp', '#3f3f99', 2),
(80, 'Nothing Else Matters - Metallica', 'https://localhost:7021/uploads/09aeddc981accf76c9800da279e59827b7f2455b96be6c0ec5b6629b4266d2c3.webm', 'https://localhost:7021/uploads/e88dc539af805f7f5d44cc3f3dbd678d540e996cbc6bab99d502251d1e7e2b34.webp', '#5c5c5c', 3),
(81, 'One - Metallica', 'https://localhost:7021/uploads/7f10a858d9187d01d0db5684370b15f774f7bddae1b7bd7d80ac957c3b4b15ab.webm', 'https://localhost:7021/uploads/32e4bed444b8becdc8cd2d5b442ef50fb2ef8e43494962c00912725c19da630d.webp', '#d1d1d1', 2),
(82, 'Sad But True - Metallica', 'https://localhost:7021/uploads/2e8525a7388dee2b9b11e481ac23b915fc30ee983ed88048ca3a49fdbce9fa67.webm', 'https://localhost:7021/uploads/e88dc539af805f7f5d44cc3f3dbd678d540e996cbc6bab99d502251d1e7e2b34.webp', '#5c5c5c', 1),
(83, 'The Unforgiven - Metallica', 'https://localhost:7021/uploads/e3aaae911114343f0ce73f551746f30472ff891ba2f292b6d72a9657f60b6c79.webm', 'https://localhost:7021/uploads/e88dc539af805f7f5d44cc3f3dbd678d540e996cbc6bab99d502251d1e7e2b34.webp', '#5c5c5c', 1),
(84, 'Seek & Destroy - Metallica', 'https://localhost:7021/uploads/9f1a4afdde9071ab82a9c296b0a997219fce20e0fdb95e7a4cf81c81cdcefc3c.webm', 'https://localhost:7021/uploads/29775bfc09d2f7183f331da9e7bfad8c5fb1edb1a9bb454118e0713437e3ecf8.webp', '#b80000', 2),
(85, 'About A Girl - Nirvana', 'https://localhost:7021/uploads/12c843bccf6c0948f0cc9fcf130f7d818a280af6787c55c7508c4fc4cd94fe74.webm', 'https://localhost:7021/uploads/76009010ac7a270edbf98798f13b6ae0c8ee4a2569c67f5beaebc8bd542a7d93.webp', '#cfcfcf', 1),
(86, 'Come As You Are - Nirvana', 'https://localhost:7021/uploads/a7bfb2162264dabd1ede0a18602a4ed85ab2ae22e65ac53f7ea72f190b9caf53.webm', 'https://localhost:7021/uploads/cc258415b40df36cd6c74bc6671f5bed55c3835da04c1450131b3ab1c33e19ed.webp', '#3caafa', 3),
(87, 'Dumb - Nirvana', 'https://localhost:7021/uploads/7ed35e49343d87d4a6ec2769b18ba09395c809d2745ebb1474d67f339dc4e0b5.webm', 'https://localhost:7021/uploads/43cf05710a9cb442a223170f6f6e008be582fc34e4ec538db6a987c0afb3c1d6.webp', '#fff2b8', 1),
(88, 'In Bloom - Nirvana', 'https://localhost:7021/uploads/ec2c830b31535b71aeaf37c523586cab307fd2f6d834b95e3961cbfc2607991d.webm', 'https://localhost:7021/uploads/cc258415b40df36cd6c74bc6671f5bed55c3835da04c1450131b3ab1c33e19ed.webp', '#3caafa', 0),
(89, 'Lithium - Nirvana', 'https://localhost:7021/uploads/78cbab90344a39eab98dee3de3b35b59d237cfd5b845fd59f5e3197f2663687a.webm', 'https://localhost:7021/uploads/cc258415b40df36cd6c74bc6671f5bed55c3835da04c1450131b3ab1c33e19ed.webp', '#3caafa', 1),
(90, 'Polly - Nirvana', 'https://localhost:7021/uploads/4b357dc4d98dd21417a36e5b1013fb5b3c6ac89d0da6e0add34c0153e6bbbaba.webm', 'https://localhost:7021/uploads/cc258415b40df36cd6c74bc6671f5bed55c3835da04c1450131b3ab1c33e19ed.webp', '#3caafa', 0),
(91, 'You Know You Are Right - Nirvana', 'https://localhost:7021/uploads/72bc19938b89b12bd4c6d12f4761a683d1e4a3cc1fd2b79c2edb341c07a98c9a.webm', 'https://localhost:7021/uploads/b639ae3b815900a1e0b76685d2bac780a9fec3d49d3172325bbf14b8f91092ca.webp', '#3e3e3e', 1),
(92, 'Like That - Future ft. Kendrick Lamar', 'https://localhost:7021/uploads/705447045f6357dacad18bebe32d2e4a0b2cf22dd8228dcb0117328a78e881a4.webm', 'https://localhost:7021/uploads/55f68c9ef81d59ca29b7caa14fc2871abf5466ca7c7ef4fbb1edf1e290e2d959.webp', '#0d0d4b', 1),
(93, 'Luther - Kendrick Lamar ft. SZA', 'https://localhost:7021/uploads/fa62576b1ec7cc5aea0f9be043c3c7ea2278051b28139f7ce85bc17c8d22a9a5.webm', 'https://localhost:7021/uploads/e83c082606b6271644163969ffe1bfe5299a098ce73b16a0622403bcb566c468.webp', '#d4d4d4', 1),
(94, 'Not Like Us - Kendrick Lamar', 'https://localhost:7021/uploads/b16e5bffacc19cec2b8758f856730fb0e38c48965195de3e3eadaa8b6385ab0e.webm', 'https://localhost:7021/uploads/addf1d064a3a6945e9a7c8c8fcec74eee8e9194aca37985f77eac86071b28170.webp', '#d4d4d4', 2),
(95, 'TV Off - Kendrick Lamar', 'https://localhost:7021/uploads/6c3ddf7eaa2eb35c0f247b36cfc27e3df3f3b3d511ffdfa54867f572f9374cb6.webm', 'https://localhost:7021/uploads/e83c082606b6271644163969ffe1bfe5299a098ce73b16a0622403bcb566c468.webp', '#d4d4d4', 0),
(96, 'All The Stars - Kendrick Lamar ft. SZA', 'https://localhost:7021/uploads/27f56a72ef428095550ddde7e9cbc25db501d45fd2381a7cbbabd796c1887909.webm', 'https://localhost:7021/uploads/67a5f014d76570356b14ee3d1b0862a4f03b944c85ce243621d53dc1bc8f97dd.webp', '#d4d4d4', 1),
(97, 'Money Trees - Kendrick Lamar', 'https://localhost:7021/uploads/6c39bc6c32ad5643c4b48d82a93f8a85da0e87505da9b604a1150065f26a3db2.webm', 'https://localhost:7021/uploads/c4cabd6f3fa70d1a70cac83105fd09d496199d29abe03f5c86944751f47474e7.webp', '#d4d4d4', 2),
(98, 'Squabble Up - Kendrick Lamar', 'https://localhost:7021/uploads/e512ca082358810fdcbc92702dc9fcb21af9f0c8d62f8fe41d9f333324d11326.webm', 'https://localhost:7021/uploads/e83c082606b6271644163969ffe1bfe5299a098ce73b16a0622403bcb566c468.webp', '#d4d4d4', 1),
(99, 'Diamonds - Rihanna', 'https://localhost:7021/uploads/5665e1108db936fcf2c2fccfad17475706f678231532d146d14202b07d1c56da.webm', 'https://localhost:7021/uploads/7db72e6bc703922a3afc9ab324ec59bbfaeac2dd78dd34a5295a6b3c1546b450.webp', '#d4d4d4', 0),
(100, 'Only Girl In The World - Rihanna', 'https://localhost:7021/uploads/92fee7baa8cda94b14086325ee8cb8cf59130978b92e7bd49bfe3c49f700a917.webm', 'https://localhost:7021/uploads/a999639d3f88445644116c550278b70178c5d58a7a663186847ded2b55d1044d.webp', '#cba5e3', 0),
(101, 'Rude Boy - Rihanna', 'https://localhost:7021/uploads/bbe217a906872c6db73b9bd18072b080cfbca1c7f17f5e265c00b70963a359db.webm', 'https://localhost:7021/uploads/4e010e07d2c25f330fb92aea19cf13fbb71d0295465cd5d99a11ec266b47964f.webp', '#d4d4d4', 1),
(102, 'Stay', 'https://localhost:7021/uploads/5108bf531b1160798522ea04399913442646c55c0e92bd53432dc55b8956bdd7.webm', 'https://localhost:7021/uploads/481ca8eaf01d19f56bfeed7ee54698bc93ba122cf2091109eea6792e7ec2c09d.webp', '#c3a3ff', 0),
(103, 'Umbrella - Rihanna ft. Jay-Z', 'https://localhost:7021/uploads/3c4536cda21f38151fc46c0c2a1889c1c76fa8e0094527984a6094cd798efde4.webm', 'https://localhost:7021/uploads/67c377fa6a6f5e0d036e4511831036135348f5f20d6bca26aae2cefadfe93d4e.webp', '#000000', 1),
(104, 'We Found Love - Rihanna ft. Calvin Harris', 'https://localhost:7021/uploads/1ee82c09ae4a754f96d9a9a9426e6f66cc8abb5e86795923027e41d2bd19d376.webm', 'https://localhost:7021/uploads/b812b356de2df645d5297bdf64e94df646ee9944e78e210619e85f839c4bfdbf.webp', '#d4d4d4', 0),
(105, 'Work - Rihanna ft. Drake', 'https://localhost:7021/uploads/c8cb0f9ae3cc347f7f3f35cb1273af4e0af7f1901dd7f68e5c33a42f6c6f2d58.webm', 'https://localhost:7021/uploads/b3b864420fed92763158238ccaec7c1b3a29a9cb667b69374a19c8ba2b780544.webp', '#5a6d65', 0),
(106, 'Just The Way You Are - Bruno Mars', 'https://localhost:7021/uploads/eaf2522bc9ad7f7409a0910e04c756721cb32d272561216a0a4de71f936c039b.webm', 'https://localhost:7021/uploads/e27701f88d405fba520f3427f14b04ccdec0caa7bfef628bc4e74589fc3413d5.webp', '#faf0b4', 2),
(107, 'Locked Out Of Heaven - Bruno Mars', 'https://localhost:7021/uploads/2317bdeca082f3ea87b66cb47d907281befc3f62d8b2bde705acc2aff12f5aa1.webm', 'https://localhost:7021/uploads/af968fc8aca17f58f680124c242d52cb694ab5b24baff30988286c9f982d33a8.webp', '#faf0b4', 0),
(108, 'The Lazy Song - Bruno Mars', 'https://localhost:7021/uploads/37cf9b9375f1c4f0ba2297ed26c6191178cf7e6589c505364d015bcda25456dc.webm', 'https://localhost:7021/uploads/dcacd887073de62e338c56c87f6b3a4c49ecd20c783e52ad114118a0eaa5cd2d.webp', '#d4d4d4', 1),
(109, 'APT. - Bruno Mars ft. Rose', 'https://localhost:7021/uploads/84b5c8742d7b4fa9ac5aa3c49c0b9cfa81bb53bf9e1a8ed077e408bd19626265.webm', 'https://localhost:7021/uploads/0ae409fc28b74b8269a1b47dd56fcf77da09387325cf61be86d172645eaab1f1.webp', '#f66488', 11),
(110, 'Bad Guy - Billie Eilish', 'https://localhost:7021/uploads/e818cd394d55a0b9e4990dc521dba009568b606026fa9bdd636baf6db801858e.webm', 'https://localhost:7021/uploads/001d31bd5b675295ab04ab72b663b74b05cc443b98f58389b4c83cd65066a9ea.webp', '#470000', 2),
(111, 'Birds Of A Feather - Billie Eilish', 'https://localhost:7021/uploads/7575bf4be0ea5eccbe34a7d1bd1b5fd2bba5c395e773a11953d8ab1be81bcb69.webm', 'https://localhost:7021/uploads/b6cd99118f4c6b688e3e3d48a7bb250962faba6947ddcecd890ccfde86eca221.webp', '#d1f4ff', 1),
(112, 'Everything I Wanted - Billie Eilish', 'https://localhost:7021/uploads/2a397df1b3775f82037a710be775de39819418ea6159261db4b8cab5d2c4595b.webm', 'https://localhost:7021/uploads/3f7549490c56ff3cf164b978731f21ad4875491e4d70f550edd719003293e914.webp', '#ffddc7', 0),
(113, 'Happier Than Ever - Billie Eilish', 'https://localhost:7021/uploads/40981b295cce639fc4507e1cf6d7cd4b535c459744f23c30e67c2982680a0c39.webm', 'https://localhost:7021/uploads/eebd9f3f3d6cecb90a39e32afa09d918c7da818a334cfd8b06621dcd3abc51d1.webp', '#f5e8c2', 1),
(114, 'What Was I Made For - Billie Eilish', 'https://localhost:7021/uploads/5b9cc6bb7deacede7c1955751eb0edec03d4e1824c44a075e5e4dac18cb49d06.webm', 'https://localhost:7021/uploads/0f1322ce9c3febf0c721bec416f8664c1233592d6bd16b94169839589f676aff.webp', '#b5d973', 0),
(115, 'Lovely - Billie Eilish ft. Khalid', 'https://localhost:7021/uploads/5ca96dfb94bc8f9044c4560dfb09430d2b2bbb90cd095d293d69ab3b4059374c.webm', 'https://localhost:7021/uploads/123ecbb5dbd3c2715bad0aa99baf047a46127878137827418c80625d4d32ff34.webp', '#fef4d7', 1),
(116, 'Addicted To You - Avicii', 'https://localhost:7021/uploads/390c61bcae453531a4dd4e8f360f609c4bd37193e37f3df08614f1f3adc31abe.webm', 'https://localhost:7021/uploads/c4740ed67f06d1780363d0e3eb5369b3c15fe9908fb067a7592cdee8df0714ed.webp', '#c68686', 0),
(117, 'Hey Brother - Avicii', 'https://localhost:7021/uploads/d95bc726622f6b3929b344a1fcbfd74449c59d2997f52530b6b62dc48444188a.webm', 'https://localhost:7021/uploads/9aa6ce23fb54923bcd1fb562f70215a31c524235cb9731f3a7bd0fdc1bbc17c9.webp', '#608575', 0),
(118, 'Levels - Avicii', 'https://localhost:7021/uploads/20a7f40b8a0e4692e0a8a51526b17eb52806b41e0ee399cd73155caf86d5454c.webm', 'https://localhost:7021/uploads/f64d34bddd85e40452301c3e4934e76062891b0a44085912b75a67b98faf0387.webp', '#fff5b8', 1),
(119, 'Lonely Together - Avicii', 'https://localhost:7021/uploads/5e0943025c0d2246e015fc96e0a2e54a7e9a70e13f86e092574d3fb9464cb678.webm', 'https://localhost:7021/uploads/a3a63a803b4622d68634a289c97a024901cfcd6551e63227ebdc8c5232c3ad19.webp', '#d4d4d4', 0),
(120, 'The Nights - Avicii', 'https://localhost:7021/uploads/76a36017d3d0c3f4bd5f5a22facff743682ace05d7465ddfd2fe9ff55cc1c492.webm', 'https://localhost:7021/uploads/feff2e2da6247618c927b4717bf2f4be6f84dd3ca55b56c2b5af31b55acb04d1.webp', '#9effe7', 1),
(121, 'Waiting For Love - Avicii', 'https://localhost:7021/uploads/6e9c5634f011d99a319e3d0710779f83abe47f84faf2277d6373ca8a764c5e4e.webm', 'https://localhost:7021/uploads/771a058d4dd09586822bcba94ced6bfca0cc2b2e9f8dfb9594859d3cd82f218e.webp', '#9ecdff', 0),
(122, 'Wake Me Up - Avicii', 'https://localhost:7021/uploads/bd297c3296a042dec281d481e2e2c99174d13940c670cc9f428b6399dd3ad9da.webm', 'https://localhost:7021/uploads/93720b5f911a6eaac2d06290014b2a25eb591f0a1f7313e29c4d490331f557d7.webp', '#6e4545', 0),
(123, 'Crazy In Love - Beyonce ft. Jay-Z', 'https://localhost:7021/uploads/46e5543d5761e05701e581fedce1079556e0b424417706cb376cea7e1e780715.webm', 'https://localhost:7021/uploads/c3b5d81f097c238177ed67e114cf708150567e570735795c64017fa1be7e51f9.webp', '#780707', 0),
(124, 'Drunk In Love - Beyonce ft. Jay-Z', 'https://localhost:7021/uploads/baf6a8f22d696500ab0d80af3e8f649d0e92cce9000cfff7a0a417363729b44d.webm', 'https://localhost:7021/uploads/6bd5028d7190f6b32da7648ffe8e4cbc67d668a5a41b9e78114581893d4740dd.webp', '#404040', 0),
(125, 'Formation - Beyonce', 'https://localhost:7021/uploads/f4b0db9349cc3517f7831edd82cf0bb6ac05ad535222c369f8b620634f4c2130.webm', 'https://localhost:7021/uploads/98e99f34541b702310ebd2dfd030816e2d9a225682cdd4b6815838d4f991ec17.webp', '#b8eaff', 0),
(126, 'Halo - Beyonce', 'https://localhost:7021/uploads/219c35f58d63b664c105f25db7f41adb7e15e261fec57ddc9bed43d1e0b18914.webm', 'https://localhost:7021/uploads/2e2aaa8529c2f05111438e25fd9e9bcce2b75299fe7c30d837407927a1de02ef.webp', '#d4d4d4', 0),
(127, 'Irreplaceable - Beyonce', 'https://localhost:7021/uploads/0501b498fc8325fc0c6c71f03c07c72dc29d6bd74b5d74d51eeb5360140724d6.webm', 'https://localhost:7021/uploads/27d9e74c33e7485ae93c4969802da6b0270dcfc618caf2471f212417b3239963.webp', '#7d7f5c', 0),
(128, 'Love On Top - Beyonce', 'https://localhost:7021/uploads/4a1c13b4c88bd4f539a93c8821a7db47915c3e3a819202169aa4fc83d05f14a8.webm', 'https://localhost:7021/uploads/dc9f68d8330f19107da1465df8e493d1ca1633d7defe7af71eee4e65dee0ab14.webp', '#d4d4d4', 1),
(129, 'Single Ladies - Beyonce', 'https://localhost:7021/uploads/24ac7ddadf3b644ba214b160c30bbd76f83152146e2e1093e0b5744c19101f89.webm', 'https://localhost:7021/uploads/1aefb03160915800df093925f335a8db1208af9442501f0c411e0d7a9623e679.webp', '#6b6b6b', 2),
(138, 'Тигър - Иво Най-доброто', 'https://localhost:7021/uploads/49f081aad828ca08ed3b4c0ffd2f366231d95f77a02d1e6a77a68036f5ac57bb.webm', 'https://localhost:7021/uploads/0de0c160d5488e78bb55f9a6e8dd50dbdc7c4e151c733449aeae633424131d0a.webp', '#d48c8c', 2);

-- --------------------------------------------------------

--
-- Table structure for table `song_artist`
--

CREATE TABLE `song_artist` (
  `id` int(11) NOT NULL,
  `song_id` int(11) NOT NULL,
  `artist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `song_artist`
--

INSERT INTO `song_artist` (`id`, `song_id`, `artist_id`) VALUES
(59, 38, 57),
(60, 39, 57),
(61, 40, 57),
(62, 41, 57),
(63, 41, 58),
(64, 42, 57),
(66, 43, 57),
(73, 49, 71),
(74, 50, 71),
(75, 51, 71),
(76, 52, 71),
(77, 53, 71),
(78, 36, 57),
(79, 36, 119),
(80, 54, 17),
(87, 55, 120),
(88, 57, 20),
(89, 58, 29),
(90, 59, 7),
(92, 61, 20),
(93, 62, 13),
(95, 64, 14),
(96, 65, 14),
(97, 66, 14),
(98, 67, 17),
(101, 69, 29),
(102, 70, 120),
(103, 71, 120),
(104, 72, 34),
(105, 73, 34),
(108, 68, 3),
(111, 74, 20),
(112, 75, 20),
(113, 76, 20),
(114, 77, 20),
(115, 78, 18),
(116, 79, 18),
(118, 81, 18),
(119, 80, 18),
(120, 82, 18),
(121, 83, 18),
(122, 84, 18),
(123, 85, 15),
(124, 86, 15),
(125, 87, 15),
(126, 88, 15),
(127, 89, 15),
(128, 90, 15),
(129, 91, 15),
(130, 92, 31),
(131, 92, 24),
(132, 93, 24),
(133, 93, 51),
(134, 94, 24),
(135, 95, 24),
(136, 96, 24),
(137, 96, 51),
(139, 98, 24),
(140, 97, 24),
(141, 99, 55),
(142, 100, 55),
(143, 101, 55),
(144, 102, 55),
(145, 103, 30),
(146, 103, 55),
(147, 104, 36),
(148, 104, 55),
(149, 105, 26),
(150, 105, 55),
(153, 107, 7),
(154, 108, 7),
(155, 109, 7),
(156, 110, 5),
(157, 111, 5),
(158, 112, 5),
(159, 113, 5),
(160, 114, 5),
(161, 115, 5),
(162, 116, 35),
(163, 117, 35),
(164, 118, 35),
(165, 119, 35),
(166, 120, 35),
(167, 121, 35),
(168, 122, 35),
(173, 123, 48),
(174, 123, 30),
(175, 124, 48),
(176, 124, 30),
(177, 125, 48),
(178, 126, 48),
(179, 127, 48),
(180, 128, 48),
(181, 129, 48),
(184, 60, 20),
(192, 138, 121),
(193, 63, 13);

-- --------------------------------------------------------

--
-- Table structure for table `song_genre`
--

CREATE TABLE `song_genre` (
  `id` int(11) NOT NULL,
  `song_id` int(11) NOT NULL,
  `genre_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `song_genre`
--

INSERT INTO `song_genre` (`id`, `song_id`, `genre_id`) VALUES
(52, 38, 6),
(53, 39, 6),
(54, 40, 6),
(55, 41, 6),
(56, 42, 6),
(58, 43, 6),
(66, 49, 6),
(67, 50, 6),
(68, 51, 2),
(69, 51, 6),
(70, 52, 2),
(71, 53, 6),
(72, 36, 6),
(73, 54, 2),
(80, 55, 3),
(81, 57, 2),
(82, 58, 3),
(83, 59, 1),
(85, 61, 2),
(86, 62, 2),
(88, 64, 2),
(89, 65, 2),
(90, 66, 2),
(91, 67, 2),
(93, 69, 3),
(94, 70, 3),
(95, 71, 3),
(96, 72, 3),
(97, 73, 3),
(99, 68, 3),
(102, 74, 2),
(103, 75, 2),
(104, 76, 2),
(105, 77, 2),
(106, 78, 2),
(107, 79, 2),
(109, 81, 2),
(110, 80, 2),
(111, 82, 2),
(112, 83, 2),
(113, 84, 2),
(114, 85, 2),
(115, 86, 2),
(116, 87, 2),
(117, 88, 2),
(118, 89, 2),
(119, 90, 2),
(120, 91, 2),
(121, 92, 3),
(122, 93, 3),
(123, 94, 3),
(124, 95, 3),
(125, 96, 3),
(127, 98, 3),
(128, 97, 3),
(129, 99, 5),
(130, 100, 5),
(131, 101, 5),
(132, 102, 5),
(133, 103, 5),
(134, 104, 5),
(135, 105, 5),
(137, 106, 1),
(138, 107, 1),
(139, 108, 1),
(140, 109, 1),
(141, 110, 1),
(142, 111, 1),
(143, 112, 1),
(144, 113, 1),
(145, 114, 1),
(146, 115, 1),
(147, 116, 4),
(148, 117, 4),
(149, 118, 4),
(150, 119, 4),
(151, 120, 4),
(152, 121, 4),
(153, 122, 4),
(159, 123, 1),
(160, 123, 3),
(161, 123, 5),
(162, 124, 3),
(163, 124, 5),
(164, 125, 3),
(165, 126, 1),
(166, 126, 5),
(167, 127, 1),
(168, 127, 5),
(169, 128, 5),
(170, 129, 1),
(171, 129, 5),
(174, 60, 2),
(182, 138, 29),
(183, 63, 2);

-- --------------------------------------------------------

--
-- Table structure for table `song_trending`
--

CREATE TABLE `song_trending` (
  `song_id` int(11) NOT NULL,
  `title` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `cover_url` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `song_url` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `song_trending`
--

INSERT INTO `song_trending` (`song_id`, `title`, `cover_url`, `song_url`) VALUES
(44, 'Djena ft. Djordan - Huligani ', 'https://i.ytimg.com/vi/8_iq4wnadUM/mqdefault.jpg', 'https://localhost:7021/uploads/trending/87f025615dcafb3de173a37c2e5ea7beb9b4d875633d80c73fa6eceb8de01551.mp3'),
(45, 'Emanuela x Tedi Aleksandrova - Danoto kopay ', 'https://i.ytimg.com/vi/R3Vk7sec_2k/mqdefault.jpg', 'https://localhost:7021/uploads/trending/43007b11dd4c686defe2bc75dbadeb9f13f2dd8633414b65ed7953ccca22af08.mp3'),
(46, 'SIMONA - KATINARA ', 'https://i.ytimg.com/vi/W7lDPMjI_rU/mqdefault.jpg', 'https://localhost:7021/uploads/trending/55f07f6c9ed75b766ff02aa1447cd74aaeda92175550d31c1e06732e315f77ae.mp3'),
(47, 'VANE$$A feat. SOFI MARINOVA - VANESA REJE, REJE ', 'https://i.ytimg.com/vi/GErn5UAQW0A/mqdefault.jpg', 'https://localhost:7021/uploads/trending/16f15ade9241c95c53dadc4392e2bb90a7c104965783c120803b2c013b6ed3c9.mp3'),
(48, 'N.A.S.I. - PO-DOBRE ', 'https://i.ytimg.com/vi/gXDAFL3eFMc/mqdefault.jpg', 'https://localhost:7021/uploads/trending/e2f489c3b662815f8d561997ac8de4416c9fab1a5a7acfb6cd992a82b750ccd7.mp3');

-- --------------------------------------------------------

--
-- Table structure for table `user_artist_activity`
--

CREATE TABLE `user_artist_activity` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `artist_id` int(11) NOT NULL,
  `clicks` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_artist_activity`
--

INSERT INTO `user_artist_activity` (`id`, `user_id`, `artist_id`, `clicks`) VALUES
(48, 30, 57, 18),
(49, 30, 7, 21),
(50, 30, 71, 52),
(51, 37, 20, 9),
(52, 37, 57, 5),
(53, 37, 58, 2),
(54, 37, 13, 1),
(55, 37, 14, 1),
(56, 37, 71, 3),
(57, 37, 34, 2),
(58, 37, 3, 1),
(59, 37, 120, 1),
(66, 30, 29, 3),
(73, 30, 34, 1),
(74, 30, 20, 14),
(75, 30, 2, 2),
(76, 30, 120, 1),
(77, 30, 14, 4),
(78, 30, 13, 1),
(79, 30, 18, 10),
(80, 30, 17, 6),
(81, 37, 18, 1),
(82, 30, 15, 7),
(83, 30, 31, 1),
(84, 30, 24, 8),
(89, 30, 55, 2),
(92, 30, 35, 1),
(93, 30, 48, 2),
(94, 30, 5, 4),
(95, 30, 64, 1),
(96, 30, 12, 1),
(97, 48, 57, 2),
(98, 48, 119, 2),
(99, 48, 20, 1),
(100, 30, 121, 2),
(101, 37, 7, 3),
(102, 37, 2, 1),
(103, 37, 35, 1),
(104, 37, 5, 1),
(105, 37, 48, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user_genre_activity`
--

CREATE TABLE `user_genre_activity` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `genre_id` int(11) NOT NULL,
  `clicks` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_genre_activity`
--

INSERT INTO `user_genre_activity` (`id`, `user_id`, `genre_id`, `clicks`) VALUES
(17, 30, 6, 70),
(18, 30, 1, 44),
(19, 37, 2, 40),
(20, 37, 4, 4),
(21, 37, 6, 11),
(22, 37, 3, 5),
(23, 37, 1, 7),
(24, 30, 3, 18),
(25, 30, 2, 60),
(26, 30, 4, 7),
(27, 30, 5, 11),
(28, 48, 6, 2),
(29, 48, 2, 4),
(30, 48, 1, 1),
(31, 30, 29, 2),
(32, 37, 5, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user_info`
--

CREATE TABLE `user_info` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(100) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `avatar_url` varchar(255) NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user_info`
--

INSERT INTO `user_info` (`user_id`, `username`, `email`, `password_hash`, `avatar_url`, `created_at`) VALUES
(30, 'admin', 'prasetosvinq@gmail.com', '+q5zLaELihDgH9a04I+w0EJFtwCUEVbRBIG2oKZb9aGp0FfwjsmmI5KXs/QyiSqo', 'https://localhost:7021/uploads/avatar_admin.webp', '2025-02-05 12:31:12'),
(37, 'Гост', 'guest@gmail.com', 'uEZKFVB+M8bRYZ7NEbMpW86JTzqcGsRI+v90+t3jdXzR91N/Le05m+InUrH6KH41', 'https://localhost:7021/uploads/avatar_guest.webp', '2025-02-05 13:09:22'),
(48, 'Ivo Radev', 'ivoradev@gmail.com', 'kbhqzTr153mRzrxgbsLqRTRGo6fPZLe+YB9ktD5a2xfTebyJJExQJCY3HM6YJWsf', 'https://localhost:7021/uploads/avatar_rock.webp', '2025-04-06 08:09:27');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `artist`
--
ALTER TABLE `artist`
  ADD PRIMARY KEY (`artist_id`);

--
-- Indexes for table `artist_genre`
--
ALTER TABLE `artist_genre`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_genre_id` (`genre_id`),
  ADD KEY `artist_genre_ibfk_1` (`artist_id`);

--
-- Indexes for table `favourite`
--
ALTER TABLE `favourite`
  ADD PRIMARY KEY (`id`),
  ADD KEY `favourite_ibfk_1` (`user_id`),
  ADD KEY `favourite_ibfk_2` (`song_id`);

--
-- Indexes for table `genre`
--
ALTER TABLE `genre`
  ADD PRIMARY KEY (`genre_id`);

--
-- Indexes for table `song`
--
ALTER TABLE `song`
  ADD PRIMARY KEY (`song_id`);

--
-- Indexes for table `song_artist`
--
ALTER TABLE `song_artist`
  ADD PRIMARY KEY (`id`),
  ADD KEY `song_artist_ibfk_1` (`song_id`),
  ADD KEY `song_artist_ibfk_2` (`artist_id`);

--
-- Indexes for table `song_genre`
--
ALTER TABLE `song_genre`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_genre_id_song` (`genre_id`),
  ADD KEY `song_genre_ibfk_1` (`song_id`);

--
-- Indexes for table `song_trending`
--
ALTER TABLE `song_trending`
  ADD PRIMARY KEY (`song_id`);

--
-- Indexes for table `user_artist_activity`
--
ALTER TABLE `user_artist_activity`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `user_id` (`user_id`,`artist_id`),
  ADD KEY `user_artist_activity_ibfk_2` (`artist_id`);

--
-- Indexes for table `user_genre_activity`
--
ALTER TABLE `user_genre_activity`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `user_id` (`user_id`,`genre_id`),
  ADD KEY `fk_genre_id_activity` (`genre_id`);

--
-- Indexes for table `user_info`
--
ALTER TABLE `user_info`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `username` (`username`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `artist`
--
ALTER TABLE `artist`
  MODIFY `artist_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=122;
--
-- AUTO_INCREMENT for table `artist_genre`
--
ALTER TABLE `artist_genre`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=246;
--
-- AUTO_INCREMENT for table `favourite`
--
ALTER TABLE `favourite`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;
--
-- AUTO_INCREMENT for table `genre`
--
ALTER TABLE `genre`
  MODIFY `genre_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;
--
-- AUTO_INCREMENT for table `song`
--
ALTER TABLE `song`
  MODIFY `song_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=139;
--
-- AUTO_INCREMENT for table `song_artist`
--
ALTER TABLE `song_artist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=194;
--
-- AUTO_INCREMENT for table `song_genre`
--
ALTER TABLE `song_genre`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=184;
--
-- AUTO_INCREMENT for table `song_trending`
--
ALTER TABLE `song_trending`
  MODIFY `song_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;
--
-- AUTO_INCREMENT for table `user_artist_activity`
--
ALTER TABLE `user_artist_activity`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=106;
--
-- AUTO_INCREMENT for table `user_genre_activity`
--
ALTER TABLE `user_genre_activity`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;
--
-- AUTO_INCREMENT for table `user_info`
--
ALTER TABLE `user_info`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `artist_genre`
--
ALTER TABLE `artist_genre`
  ADD CONSTRAINT `artist_genre_ibfk_1` FOREIGN KEY (`artist_id`) REFERENCES `artist` (`artist_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_genre_id` FOREIGN KEY (`genre_id`) REFERENCES `genre` (`genre_id`) ON DELETE CASCADE;

--
-- Constraints for table `favourite`
--
ALTER TABLE `favourite`
  ADD CONSTRAINT `favourite_ibfk_2` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `favourite_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_info` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `song_artist`
--
ALTER TABLE `song_artist`
  ADD CONSTRAINT `song_artist_ibfk_2` FOREIGN KEY (`artist_id`) REFERENCES `artist` (`artist_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `song_artist_ibfk_1` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`) ON DELETE CASCADE;

--
-- Constraints for table `song_genre`
--
ALTER TABLE `song_genre`
  ADD CONSTRAINT `song_genre_ibfk_1` FOREIGN KEY (`song_id`) REFERENCES `song` (`song_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_genre_id_song` FOREIGN KEY (`genre_id`) REFERENCES `genre` (`genre_id`) ON DELETE CASCADE;

--
-- Constraints for table `user_artist_activity`
--
ALTER TABLE `user_artist_activity`
  ADD CONSTRAINT `user_artist_activity_ibfk_2` FOREIGN KEY (`artist_id`) REFERENCES `artist` (`artist_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `user_artist_activity_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_info` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `user_genre_activity`
--
ALTER TABLE `user_genre_activity`
  ADD CONSTRAINT `fk_genre_id_activity` FOREIGN KEY (`genre_id`) REFERENCES `genre` (`genre_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `user_genre_activity_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user_info` (`user_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
