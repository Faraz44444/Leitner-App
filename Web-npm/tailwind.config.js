module.exports = {
    content: ["./Pages/**/*.{html,js}"],
    //purge: [],
    darkMode: false, // or 'media' or 'class'
    theme: {
        screens: {
            sm: '480px',
            md: '768px',
            lg: '976px',
            xl: '1440px',
        },
        extend: {
            zIndex: {
                '9998': '9998'
            },
            width: {
                '300': '300px'
            },
            transform: {
                'rotate-y-180': 'rotateY(180deg)'
            },
            transformStyle: ['responsive']
        },
        minHeight: {
            '1/2': '50%',
            '1': '80vh'

        },
        minWidth: {
            '1/2': '50vw',
            '1/10': '5vw'
        },
        //        height: {
        //            fit: 'fit-content',
        //            '7em': '1.75em'
        //        },
        //        width: {
        //            fit: 'fit-content',
        //            '7em': '1.75em'
        //        },

        colors: {
            coolGray: {
                50: "#F9FAFB",
                100: "#F3F4F6",
                150: "#EBECF0",
                200: "#E5E7EB",
                300: "#D1D5DB",
                400: "#9CA3AF",
                500: "#6B7280",
                600: "#4B5563",
                700: "#374151",
                800: "#1F2937",
                900: "#111827",
            },
            blueGray: {
                50: "#F8FAFC",
                100: "#F1F5F9",
                200: "#E2E8F0",
                300: "#CBD5E1",
                400: "#94A3B8",
                500: "#64748B",
                600: "#475569",
                700: "#334155",
                800: "#1E293B",
                900: "#0F172A",
            },
            purple: {
                1: "#1e0f3d",
                2: "#3b2356"
            },
            background: {
                1: "#29292f",
                2: "#67647f",
                3: "#221887"
            },
            golden: {
                1: "#f5970f",
                2: "#f7A837"
            },
            border: {
                1: "#ffa07a"
            }
        }
    },
    variants: {
        extend: {
            backgroundColor: ['odd'],
            display: ['group-hover', 'group-focus', 'focus-within', 'hover'],
            textColor: ['group-focus'],
        },
    },
    plugins: [
        function ({ addUtilities }) {
            const newUtilities = {
                '.transform-style-3d': {
                    'transform-style': 'preserve-3d',
                },
            };

            addUtilities(newUtilities);
        },
    ],
    plugins: [
        require("@tailwindcss/forms")({
            strategy: 'class',
        })
    ],
}
