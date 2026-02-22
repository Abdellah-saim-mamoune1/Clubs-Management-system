import { useState, useCallback } from "react";
import { useNavigate } from "react-router-dom";
import { useAppSelector } from "../../Slices/Hooks";
import { GraduationCap, Calendar, Users, Camera, X } from "lucide-react";
import { Footer } from "../../Components/Footer";
import Cropper from "react-easy-crop";

export function Account() {
  const clientInfo = useAppSelector((s) => s.ClientInfoSlice.ClientInfo);
  const navigate = useNavigate();
  const [uploading, setUploading] = useState(false);
  const [imageUrl] = useState(clientInfo?.imageUrl || "");
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [croppedAreaPixels, setCroppedAreaPixels] = useState<any>(null);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);
  const [isCropping, setIsCropping] = useState(false);
  const [crop, setCrop] = useState({ x: 0, y: 0 });
  const [zoom, setZoom] = useState(1);

  if (!clientInfo) {
    return (
      <div className="w-full h-full flex items-center justify-center py-6">
        <div className="w-6 h-6 border-2 border-teal-600 border-t-transparent rounded-full animate-spin"></div>
      </div>
    );
  }
  
  // Handle image selection
  const handleImageChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;
    setSelectedFile(file);
    const reader = new FileReader();
    reader.onload = () => {
      setPreviewUrl(reader.result as string);
      setIsCropping(true);
    };
    reader.readAsDataURL(file);
  };

  // Handle saving cropped image
  const handleCropSave = useCallback(async () => {
    if (!previewUrl || !croppedAreaPixels || !selectedFile) return;

    try {
      setUploading(true);
      setIsCropping(false); 
      
    } catch (err) {
      console.error("Crop upload failed:", err);
    } finally {
      setUploading(false);
      setPreviewUrl(null);
      setSelectedFile(null);
     
    }
  }, [croppedAreaPixels, previewUrl, selectedFile, imageUrl]);

  return (
    <div className="flex flex-col min-h-screen bg-gray-50">
      <div className="flex-1 w-full px-4 md:px-10 xl:px-60 py-8">
        <div className="bg-white rounded-2xl shadow-sm p-6 space-y-8">
          {/* Profile Section */}
          <div className="flex flex-col sm:flex-row items-center sm:items-start gap-6 border-b border-b-gray-300 pb-6">
            <div className="relative">
              <img
                src={"https://wallpapers.com/images/hd/generic-person-icon-profile-ulmsmhnz0kqafcqn-2.jpg"}
                alt="Profile"
                className="w-28 h-28 rounded-full border-4 border-blue-600 shadow-md cursor-pointer object-contain bg-white"
                onClick={() =>
                  !uploading && document.getElementById("imageInput")?.click()
                }
              />

              <div
                className="absolute bottom-1 right-1 bg-blue-600 p-2 rounded-full text-white cursor-pointer"
                onClick={() =>
                  !uploading && document.getElementById("imageInput")?.click()
                }
              >
                <Camera className="w-4 h-4" />
              </div>

              <input
                id="imageInput"
                type="file"
                accept="image/*"
                className="hidden"
                onChange={handleImageChange}
              />

              {uploading && (
                <div className="absolute inset-0 bg-white/70 flex items-center justify-center rounded-full">
                  <div className="w-8 h-8 border-2 border-blue-600 border-t-transparent rounded-full animate-spin"></div>
                </div>
              )}
            </div>

            <div className="flex-1 text-center sm:text-left">
              <h1 className="text-2xl font-bold text-gray-800">
                {clientInfo.fullName}
              </h1>
              <div className="mt-2 space-y-2 text-gray-600">
                <p className="flex justify-start items-center gap-2">
                  <GraduationCap className="w-5 h-5 text-blue-600" />
                  {clientInfo.degree}
                </p>
                <p className="flex justify-start items-center gap-2">
                  <Calendar className="w-5 h-5 text-blue-600" />
                  Age: {clientInfo.age}
                </p>
              </div>
            </div>
          </div>

          {/* Joined Clubs Section */}
          <div>
            <h2 className="text-xl font-semibold text-gray-800 mb-4 flex items-center gap-2">
              <Users className="w-6 h-6 text-blue-600" /> Joined Clubs
            </h2>
            {clientInfo.joinedClubs.length === 0 ? (
              <p className="text-gray-500 text-center py-10">
                You haven’t joined any clubs yet.
              </p>
            ) : (
              <div className="grid sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {clientInfo.joinedClubs.map((club) => (
                  <div
                    key={club.id}
                    onClick={() => navigate(`/club/${club.id}`)}
                    className="bg-white border cursor-pointer border-gray-300 rounded-xl shadow hover:shadow-lg transition p-4 flex flex-col items-center text-center hover:-translate-y-1"
                  >
                    <img
                      src={"https://snworksceo.imgix.net/bdh/7029fcf1-fa5e-4db4-8f51-5701366d3631.sized-1000x1000.jpg?ar=16%3A9&crop=faces&dpr=2&fit=crop&w=800"}
                      alt={club.name}
                      className="w-20 h-20 rounded-full object-cover"
                    />
                    <h3 className="mt-3 font-semibold text-gray-800">
                      {club.name}
                    </h3>
                    <p className="text-sm text-gray-600">{club.type}</p>
                    <span className="mt-2 inline-block px-3 py-1 text-xs rounded-full bg-blue-100 text-blue-600">
                      {club.userRole}
                    </span>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>
      </div>

      {/* Footer */}
      <Footer />

      {/* Crop Modal */}
      {isCropping && !uploading && (
        <div className="fixed inset-0 bg-black/70 flex items-center justify-center z-50">
          <div className="bg-white p-4 rounded-lg shadow-lg relative w-[90%] max-w-md h-[480px] flex flex-col">
            <button
              className="absolute top-2 right-2 z-100 text-black"
              onClick={() => setIsCropping(false)}
            >
              <X className="w-9 h-9" />
            </button>

            <div className="relative flex-1 bg-gray-100 rounded-md overflow-hidden">
              <Cropper
                image={previewUrl!}
                cropShape="round"
                aspect={1}
                crop={crop}
                zoom={zoom}
                onCropChange={setCrop}
                onZoomChange={setZoom}
                onCropComplete={(_, croppedPixels) =>
                  setCroppedAreaPixels(croppedPixels)
                }
                showGrid={false}
              />
            </div>

            {/* Zoom Slider */}
            <div className="mt-4 flex flex-col items-center">
              <input
                type="range"
                min={1}
                max={3}
                step={0.1}
                value={zoom}
                onChange={(e) => setZoom(Number(e.target.value))}
                className="w-full accent-blue-600"
              />
              <button
                onClick={handleCropSave}
                className="mt-3 bg-blue-600 text-white py-2 px-6 rounded-lg hover:bg-blue-700 transition"
              >
                Save
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
