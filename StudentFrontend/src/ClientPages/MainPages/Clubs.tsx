import { useEffect, useState, useRef, useCallback } from "react";
import { IClub, IClubType } from "../../Interfaces/ClubsIntefaces";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../Slices/Hooks";
import {
  GetClubsTypesAPI,
  GetClubsPaginatedAPI,
  GetClubsByTypeAPI,
} from "../../APIs/ClubsAPIs";
import { FiSearch, FiPlus } from "react-icons/fi";
import { Footer } from "../../Components/Footer";
import ClubImage from "../../../assets/ClubImage.jpg";

export function Clubs() {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();

  const ClubsTypes = useAppSelector((s) => s.ClubsInfoSlice.ClubsTypes);
  const IsLoggedIn = useAppSelector((s) => s.ClientInfoSlice.IsLoogedIn);

  const [selectedType, setSelectedType] = useState<number | "all">("all");
  const [searchInput, setSearchInput] = useState("");
  const [search, setSearch] = useState("");
  const [clubs, setClubs] = useState<IClub[]>([]);

  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(12);
  const [hasMore, setHasMore] = useState(true);

  const [initialLoading, setInitialLoading] = useState(true);
  const [loadingMore, setLoadingMore] = useState(false);

  const loaderRef = useRef<HTMLDivElement | null>(null);

  // Load club types
  useEffect(() => {
    dispatch(GetClubsTypesAPI());
  }, [dispatch]);

  // Fetch clubs (paginated or by type)
  const fetchClubs = useCallback(
    async (page: number, isLoadMore = false) => {
      if (isLoadMore) setLoadingMore(true);
      else setInitialLoading(true);

      try {
        if (selectedType === "all") {
          const res = await GetClubsPaginatedAPI(search, page, pageSize);
          if (res) {
            setClubs((prev) =>
              page === 1 ? res.clubs ?? [] : [...prev, ...(res.clubs ?? [])]
            );
            const total = res.totalCount ?? 0;
            setHasMore(page * pageSize < total);
          }
        } else {
          const res = await GetClubsByTypeAPI(selectedType, search);
          if (res) {
            setClubs(res ?? []);
            setHasMore(false); // no pagination when filtering by type
          }
        }
      } catch (error) {
        console.error("Error fetching clubs:", error);
      } finally {
        if (isLoadMore) setLoadingMore(false);
        else setInitialLoading(false);
      }
    },
    [selectedType, pageSize, search]
  );

  // Initial load and when filters/search change
  useEffect(() => {
    setPageNumber(1);
    setClubs([]);
    setHasMore(true);
    fetchClubs(1);
  }, [selectedType, search, fetchClubs]);

  // Infinite scroll observer
  useEffect(() => {
    if (selectedType !== "all") return; // pagination only for "all"
    const observer = new IntersectionObserver(
      (entries) => {
        if (entries[0].isIntersecting && hasMore && !loadingMore) {
          fetchClubs(pageNumber + 1, true);
          setPageNumber((prev) => prev + 1);
        }
      },
      { threshold: 0.5 } // trigger when half-visible
    );

    const current = loaderRef.current;
    if (current) observer.observe(current);

    return () => {
      if (current) observer.unobserve(current);
    };
  }, [hasMore, loadingMore, pageNumber, fetchClubs, selectedType]);

  return (
    <div className="flex flex-col min-h-screen bg-gray-50">
      <div className="py-3 px-3 space-y-6 md:px-10 xl:px-60 w-full">
        {/* Top Section */}
        <div className="bg-white p-4 mb-4 rounded-xl shadow-sm sm:px-5">
          <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between mb-4 gap-3">
            <h2 className="text-lg font-semibold text-gray-700">Explore Clubs</h2>
            {IsLoggedIn && (
              <button
                onClick={() => navigate("/create-club")}
                className="flex items-center justify-center gap-2 bg-teal-600 hover:bg-teal-700 text-white px-4 py-2 rounded-lg shadow transition"
              >
                <FiPlus size={18} />
                <span className="font-medium text-sm">Create New Club</span>
              </button>
            )}
          </div>

          {/* Search input */}
          <div className="mb-4 relative flex gap-2">
            <FiSearch className="absolute left-3 top-2.5 text-gray-400" />
            <input
              type="text"
              value={searchInput}
              onChange={(e) => setSearchInput(e.target.value)}
              onKeyDown={(e) => {
                if (e.key === "Enter") setSearch(searchInput);
              }}
              placeholder="Search clubs..."
              className="w-full pl-10 pr-4 py-2 border rounded-lg shadow-sm focus:ring-2 focus:ring-teal-500 focus:border-teal-500"
            />
          </div>

          {/* Filters */}
          <div className="mb-2 hidden sm:flex flex-wrap gap-2">
            <button
              onClick={() => setSelectedType("all")}
              className={`px-4 py-1.5 rounded-full border text-sm transition ${
                selectedType === "all"
                  ? "bg-blue-500 text-white"
                  : "bg-white text-gray-700 hover:bg-gray-100"
              }`}
            >
              All
            </button>
            {ClubsTypes &&
              ClubsTypes.map((ct: IClubType) => (
                <button
                  key={ct.id}
                  onClick={() => setSelectedType(ct.id)}
                  className={`px-4 py-1.5 rounded-full border text-sm flex items-center gap-2 transition ${
                    selectedType === ct.id
                      ? "bg-blue-500 text-white"
                      : "bg-white text-gray-700 hover:bg-gray-100"
                  }`}
                >
                  {ct.type}
                  <span className="text-xs text-gray-700 bg-gray-200 rounded-full px-2 py-0.5">
                    {ct.clubsNumber}
                  </span>
                </button>
              ))}
          </div>
        </div>

        {/* Clubs list */}
        <div className="bg-white rounded-xl shadow-sm p-6">
          {initialLoading && clubs.length === 0 ? (
            <div className="flex justify-center py-20">
              <div className="w-10 h-10 border-4 border-teal-600 border-t-transparent rounded-full animate-spin"></div>
            </div>
          ) : clubs.length === 0 ? (
            <div className="flex flex-col items-center justify-center py-20 text-gray-500">
              <img
                src="https://cdn-icons-png.flaticon.com/512/4076/4076500.png"
                alt="No clubs"
                className="w-24 h-24 mb-4 opacity-70"
              />
              <p className="text-lg font-medium">No clubs found.</p>
            </div>
          ) : (
            <div className="grid sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-5">
              {clubs.map((club: IClub) => (
                <div
                  key={club.id}
                  onClick={() => navigate(`/club/${club.id}`)}
                  className="cursor-pointer flex flex-col items-center bg-white border border-gray-300 rounded-lg shadow hover:shadow-md p-4 transition hover:-translate-y-1"
                >
                  <img
                    src={`http://localhost:8102/api/clubs/${club.id}/image`}
                    onError={(e) => {
                      e.currentTarget.src = ClubImage;
                    }}
                    className="w-20 h-20 rounded-full object-cover mb-3"
                  />
                  <h3 className="text-sm font-medium text-center">{club.name}</h3>
                </div>
              ))}
            </div>
          )}

          {/* Loader for infinite scroll */}
          {selectedType === "all" && hasMore && (
            <div ref={loaderRef} className="flex justify-center mt-6">
              {loadingMore && (
                <div className="w-8 h-8 border-2 border-teal-600 border-t-transparent rounded-full animate-spin"></div>
              )}
            </div>
          )}
        </div>
      </div>

      <Footer />
    </div>
  );
}