import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { NotificationCard } from "../../Components/NotificationCard";
import { useAppDispatch, useAppSelector } from "../../Slices/Hooks";
import {
  CheckClubRequestCreationAPI,
  CreateClubAPI,
  GetClubsTypesAPI,
} from "../../APIs/ClubsAPIs";

interface IClubCreationRequest {
  studentId: number;
  clubTypeId: number;
  clubName: string;
  image: File|null;
  description:string;
}

export function CreateClub() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const StudentId =
    useAppSelector((s) => s.ClientInfoSlice.ClientInfo?.id) ?? -1;
  const IsLoggedIn = useAppSelector((s) => s.ClientInfoSlice.IsLoogedIn);
  const ClubsTypes = useAppSelector((s) => s.ClubsInfoSlice.ClubsTypes);

  const [requestedBefore, setRequestedBefore] = useState<boolean | null>(null);
  const [ImageUrl, setimageUrl] = useState<string>("");
  const [form, setForm] = useState<IClubCreationRequest>({
    studentId: StudentId,
    clubTypeId: 0,
    clubName: "",
    image:null,
    description:"",
  });
  const [loading, setLoading] = useState(false);
  
  const [notification, setNotification] = useState({
    show: false,
    isSuccess: false,
    message: "",
  });

  useEffect(() => {
    async function LoadClubsTypes() {
      await dispatch(GetClubsTypesAPI());
    }

    async function Check() {
      const hasRequestedBefore = await CheckClubRequestCreationAPI();
      setRequestedBefore(hasRequestedBefore);
      if (!hasRequestedBefore) await LoadClubsTypes();
    }

    if (IsLoggedIn) Check();
    else setRequestedBefore(false);
  }, [IsLoggedIn, dispatch]);

  
 const handleChange = (
  e: React.ChangeEvent<
    HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement
  >
) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: name === "clubTypeId" ? Number(value) : value,
    }));
  };

 const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
  const file = e.target.files?.[0];
  if (!file) return;

  const preview = URL.createObjectURL(file);
  setimageUrl(preview);
  setForm((prev) => ({
    ...prev,
    image: file,
  
  }));
};

  const handleSubmit = async () => {
    if (!IsLoggedIn) {
      navigate("/login");
      return;
    }

    if (!form.clubName.trim() || form.clubTypeId === 0 || !form.image) {
      setNotification({
        show: true,
        isSuccess: false,
        message: "Please fill in all required fields and upload an image.",
      });
      return;
    }

    setLoading(true);
    try {
      // 1️ Upload the image
    
    const formData = new FormData();
       formData.append("clubTypeId", form.clubTypeId.toString());
       formData.append("clubName", form.clubName);
       formData.append("description", form.description);
      if (form.image) {
  formData.append("image", form.image);
}

       const success = await CreateClubAPI(formData);
   
      if (success) {
        setRequestedBefore(true);
        setNotification({
          show: true,
          isSuccess: true,
          message: "Your club creation request has been submitted successfully!",
        });
      } else {
        setNotification({
          show: true,
          isSuccess: false,
          message: "Failed to submit your request. Please try again.",
        });
      }
    } catch (error) {
      console.error("Error creating club:", error);
      setNotification({
        show: true,
        isSuccess: false,
        message: "An unexpected error occurred. Please try again later.",
      });
    } finally {
      setLoading(false);
    }
  };

  if (requestedBefore === null) {
    return (
      <div className="flex items-center justify-center h-full text-gray-600">
        Checking request status...
      </div>
    );
  }

  if (requestedBefore) {
    return (
      <div className="p-8 text-center">
        <h2 className="text-2xl font-semibold text-teal-600">
         Your club creation request has been submitted successfully!
        </h2>
        <p className="text-gray-600 mt-2">
          Please wait while the administration reviews your request.
        </p>
      </div>
    );
  }

  return (
    <>
      {/* === Notification === */}
      <NotificationCard
        message={notification.message}
        isSuccess={notification.isSuccess}
        show={notification.show}
        onClose={() =>
          setNotification((prev) => ({
            ...prev,
            show: false,
          }))
        }
      />

      <div className="p-6 bg-white rounded-xl shadow-md max-w-lg mx-auto mt-8">
        <h2 className="text-2xl font-semibold text-gray-800 mb-6">
          Request to Create a Club
        </h2>

        <div className="space-y-4">
          {/* === Club Type Select === */}
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Club Type
            </label>
            <select
              name="clubTypeId"
              value={form.clubTypeId}
              onChange={handleChange}
              required
              className="mt-1 w-full border rounded-lg p-2 focus:ring focus:ring-teal-300"
            >
              <option value={0} disabled>
                Select a club type
              </option>
              {ClubsTypes &&
                ClubsTypes.map((type) => (
                  <option key={type.id} value={type.id}>
                    {type.type} ({type.clubsNumber} existing)
                  </option>
                ))}
            </select>
          </div>

          {/* === Club Name === */}
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Club Name
            </label>
            <input
              type="text"
              name="clubName"
              value={form.clubName}
              onChange={handleChange}
              minLength={2}
              required
              placeholder="Enter club name"
              className="mt-1 w-full border rounded-lg p-2 focus:ring focus:ring-teal-300"
            />
          </div>


            <div>
            <label className="block text-sm font-medium text-gray-700">
             Description
            </label>
           <textarea
             name="description"
             value={form.description}
             onChange={handleChange}
             minLength={10}
             maxLength={200}
             required
             placeholder="Enter club description"
             rows={4}
             className="mt-1 w-full border rounded-lg p-2 focus:ring focus:ring-teal-300 resize-none"
             />
          </div>

          {/* === Image Upload === */}
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Club Image
            </label>
            <input
              type="file"
              accept="image/*"
              onChange={handleFileChange}
              required
              className="mt-1 w-full border rounded-lg p-2 focus:ring focus:ring-teal-300"
            />
           
              <img
                src={ImageUrl}
                alt="Preview"
                className="mt-3 w-full h-40 object-cover rounded-lg border"
              />
            
          </div>

          {/* === Submit Button === */}
          <button
            onClick={handleSubmit}
            disabled={loading}
            className="w-full bg-teal-500 hover:bg-teal-600 text-white font-semibold py-2 rounded-lg transition-colors"
          >
            {loading ? "Submitting..." : "Submit Request"}
          </button>
        </div>
      </div>
    </>
  );
}
