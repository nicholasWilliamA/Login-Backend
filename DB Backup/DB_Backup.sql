PGDMP     	    8                |            Login    14.2    14.2     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    21549    Login    DATABASE     g   CREATE DATABASE "Login" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_Indonesia.1252';
    DROP DATABASE "Login";
                postgres    false            �            1259    21550    User    TABLE     u   CREATE TABLE public."User" (
    "Name" text NOT NULL,
    "Username" text NOT NULL,
    "Password" text NOT NULL
);
    DROP TABLE public."User";
       public         heap    postgres    false            �          0    21550    User 
   TABLE DATA           @   COPY public."User" ("Name", "Username", "Password") FROM stdin;
    public          postgres    false    209   &       \           2606    21564    User User_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY ("Username");
 <   ALTER TABLE ONLY public."User" DROP CONSTRAINT "User_pkey";
       public            postgres    false    209            �   �   x�=�1�0  g�
�j�8"� ����B(�hM��꠹�r��;��Z0C��B��8����q��jR��nL:��*L��t�%�l;Y�P�X�*�@"�h���!6��b��D��;,�(�yV�q=�&/�H�89E9�1y�����ͫ*l� ԁ3 � f6�     