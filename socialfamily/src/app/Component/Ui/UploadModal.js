"use client";
import React, { useState } from "react";
import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  useDisclosure,
  Input,
  Select,
  SelectItem,
  Image,
} from "@nextui-org/react";
import axios from "axios";
import { BASE_URL } from "@/Constant";

const UploadModal = ({ isOpen, onOpen, onClose }) => {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [photo, setPhoto] = useState(null);
  const [visibility, setVisibility] = useState([]);

  const handleSubmit = async () => {
    try {
      const formData = new FormData();
      const data = ["Family", "Friend", "Public", "Office"];
      const Visibilitydata = visibility.map((ele) => {
        return data[parseInt(ele.replace(/\D/g, ""))];
      });
      console.log(Visibilitydata);

      formData.append("Title", title);
      formData.append("Description", description);
      formData.append("Photo", photo);
      formData.append("authorId", localStorage.getItem("id"));
      formData.append("Visibility", Visibilitydata);
      // You may need to change the URL based on your backend endpoint
      await axios.post(BASE_URL + "/api/Post", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      onClose();
    } catch (error) {
      console.error("Error uploading photo:", error);
    }
  };
  const [imgurl, setImgUrl] = useState("");
  const handleFileChange = (event) => {
    event.preventDefault();
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = (e) => {
      setImgUrl(e.target.result);
    };
    reader.readAsDataURL(file);
    setPhoto(file);
  };

  return (
    <Modal size="full" isOpen={isOpen} onClose={onClose} className="  ">
      <ModalContent className="p-6 h-screen mb-14 bg-zinc-950 lg:bg-transparent ">
        {(onClose) => (
          <>
            <ModalHeader className="hidden  lg:flex flex-col gap-1">
              Upload your photo
            </ModalHeader>
            <div className="grid  grid-cols-5  w-full lg:w-8/12 mx-auto bg-zinc-950  my-auto h-screen lg:h-[600px] rounded-md">
              <div className="col-span-5 lg:col-span-3 flex items-center justify-center bg-yellow-500 h-full rounded-e-xl ">
                <Image
                  src={
                    imgurl ||
                    "https://t3.ftcdn.net/jpg/03/45/05/92/360_F_345059232_CPieT8RIWOUk4JqBkkWkIETYAkmz2b75.jpg"
                  }
                  className="w-5/12 h-5/12 object-cover mx-auto"
                />
              </div>

              <div className="col-span-5 lg:col-span-2 flex items-center flex-col gap-y-4 justify-center px-6">
                <Input
                  type="file"
                  accept="image/*"
                  onChange={handleFileChange}
                />
                <Input
                  label="Title"
                  type="text"
                  value={title}
                  onChange={(e) => setTitle(e.target.value)}
                />

                <Input
                  type="text"
                  label="Description"
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                />

                <Select
                  label="Visibility"
                  selectionMode="multiple"
                  selectedKeys={visibility}
                  value={visibility}
                  onChange={(e) => {
                    setVisibility(e.target.value.split(","));
                    console.log(visibility);
                  }}
                >
                  {["Family", "Friend", "Public", "Office"].map((ele) => {
                    return <SelectItem value={ele}>{ele}</SelectItem>;
                  })}
                </Select>
              </div>
              <div className="col-span-5 flex justify-center lg:justify-end items-center px-6 gap-x-6">
                <Button color="warning" variant="light" onPress={onClose}>
                  Cancel
                </Button>
                <Button color="warning" onPress={handleSubmit}>
                  Upload
                </Button>
              </div>
            </div>
          </>
        )}
      </ModalContent>
    </Modal>
  );
};

export default UploadModal;
