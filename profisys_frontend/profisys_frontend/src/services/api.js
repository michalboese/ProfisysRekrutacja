import axios from "axios";

export async function getDocuments() {
  try {
    const response = await axios.get(
      "http://localhost:5179/api/ReadData/GetDocuments"
    );
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
}

export async function getDocumentItems() {
  try {
    const response = await axios.get(
      "http://localhost:5179/api/ReadData/GetDocumentItemsAll"
    );
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
}
